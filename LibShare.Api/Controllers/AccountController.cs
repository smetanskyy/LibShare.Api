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
        [ProducesResponseType(typeof(TokenApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
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
        [ProducesResponseType(typeof(TokenApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("refreshtoken")]
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
        /// <returns>Returns object with instruction</returns>
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
            var test = Request.HttpContext.Connection.RemoteIpAddress;
            var result = await _accountService.RestorePasswordSendLinkOnEmailAsync(model.Email);
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

        /// <summary>
        /// Confirm user email Part 1. Send confirm link on email.
        /// </summary>
        /// <returns>Returns object with instruction</returns>
        /// <response code="200">Confirm link have been sent on email.</response>
        /// <response code="400">Confirm link haven't been sent on email.</response>
        /// <response code="409">Conflict. User is deleted. Returns message with error.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("confirm-link")]
        public async Task<IActionResult> ConfirmMailSendLinkOnEmail([FromBody] EmailApiModel model)
        {
            var validator = new EmailValidator(_recaptcha, _resourceManager);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var result = await _accountService.ConfirmMailSendLinkOnEmailAsync(model.Email);
            return Ok(result);
        }

        /// <summary>
        /// Confirm user email Part 2.
        /// </summary>
        /// <returns>Returns object with user token and refresh token.</returns>
        /// <response code="200">Email have been confirmed.</response>
        /// <response code="400">Email haven't been confirmed.</response>
        /// <response code="409">Conflict. User is deleted. Returns message with error.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(TokenApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("confirm-mail")]
        public async Task<IActionResult> ConfirmEmailBase([FromBody] ConfirmApiModel model)
        {
            var validator = new EmailValidator(_recaptcha, _resourceManager);
            var validResult = validator.Validate(new EmailApiModel { Email = model.Email, RecaptchaToken = model.RecaptchaToken });

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var result = await _accountService.ConfirmMailBaseAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Delete current account.
        /// </summary>
        /// <returns>Returns object with message</returns>
        /// <response code="200">Returns if current user has been successfully deleted.</response>
        /// <response code="400">If the request to set the user profile is incorrect.</response>
        /// <response code="401">If user is unauthorized or token is bad/expired.</response>
        /// <response code="404">If user not found.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("delete-me")]
        [Authorize]
        public async Task<IActionResult> DeleteMySelf()
        {
            var userId = User.FindFirst("id")?.Value;
            var result = await _accountService.DeleteUserByIdAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Log out of system.
        /// </summary>
        /// <returns>Returns object with message.</returns>
        /// <response code="200">Returns object with message.</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="401">If user is unauthorized or token is bad/expired.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("logout")]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst("id")?.Value;
            var logoutResult = await _accountService.LogoutUserAsync(userId);
            return Ok(logoutResult);
        }
    }
}
