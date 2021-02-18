using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace LibShare.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService<TokenApiModel> _accountService;
        private readonly IRecaptchaService _recaptcha;
        private readonly ResourceManager _resourceManager;

        public AccountController(IAccountService<TokenApiModel> accountService,
            IRecaptchaService recaptcha,
            ResourceManager resourceManager)
        {
            _accountService = accountService;
            _recaptcha = recaptcha;
            _resourceManager = resourceManager;
        }

        /// <summary>
        /// Logins user into system.
        /// </summary>
        /// <returns>Returns object with user token and refresh token.</returns>
        /// <response code="200">Returns object with tokens.</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="409">Conflict. User is deleted. Returns message with error.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginApiModel model)
        {
            var validator = new LoginValidator(_recaptcha, _resourceManager);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var loginResult = await _accountService.LoginUserAsync(model);
            return Ok(loginResult);
        }

        /// <summary>
        /// Registers a new user and logs in.
        /// </summary>
        /// <returns>Returns object with user token and refresh token.</returns>
        /// <response code="201">Returns object with tokens.</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="409">Conflict. User is deleted. Returns message with error.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(TokenApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserRegisterApiModel model)
        {
            var validator = new RegisterValidator(_recaptcha, _resourceManager);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var RegisterResult = await _accountService.RegisterUserAsync(model);
            return Created("", RegisterResult);
        }

        /// <summary>
        /// Refreshes tokens.
        /// </summary>
        /// <returns>Returns object with user token and refresh token.</returns>
        /// <response code="200">Returns object with tokens.</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("refreshtoken")]
        [ProducesResponseType(typeof(TokenApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] TokenApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new MessageApiModel()
                {
                    Message = ModelState.Select(x => x.Value.Errors).FirstOrDefault().ToString()
                });
            }

            var resultRefreshToken = await _accountService.RefreshTokenAsync(model);
            return Ok(resultRefreshToken);
        }


        /// <summary>
        /// Restores user password Part 1. Get restore link.
        /// </summary>
        /// <returns>Returns object with restore link</returns>
        /// <response code="200">Restore link have been sent on email.</response>
        /// <response code="400">Restore link haven't been sent on email.</response>
        /// <response code="409">Conflict. User is deleted. Returns message with error.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("restore-link")]
        public async Task<IActionResult> RestorePasswordSendLinkOnEmail([FromBody] EmailApiModel model)
        {
            var validator = new EmailValidator(_recaptcha, _resourceManager);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var result = await _accountService.RestorePasswordSendLinkOnEmailAsync(model.Email, Request);
            return Ok(result);
        }

        /// <summary>
        /// Restores user password Part 2. Set new password.
        /// </summary>
        /// <returns>Returns object with user token and refresh token.</returns>
        /// <response code="200">Password have been restored.</response>
        /// <response code="400">Password haven't been restored.</response>
        /// <response code="409">Conflict. User is deleted. Returns message with error.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(TokenApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("restore-password")]
        public async Task<IActionResult> RestorePasswordBase([FromBody] RestoreApiModel model)
        {
            var validator = new RestoreValidator(_recaptcha, _resourceManager);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var result = await _accountService.RestorePasswordBaseAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Change the authorized user password.
        /// </summary>
        /// <returns>Returns object with user token and refresh token.</returns>
        /// <response code="200">Password have been updated.</response>
        /// <response code="400">Password haven't been updated.</response>
        /// <response code="401">If user is unauthorized or token is bad/expired.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(TokenApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordApiModel model)
        {
            var validator = new ChangePasswordValidator(_recaptcha, _resourceManager);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var userId = User.FindFirst("id")?.Value;
            var result = await _accountService.ChangeUserPasswordAsync(model, userId);
            return Ok(result);
        }
    }
}
