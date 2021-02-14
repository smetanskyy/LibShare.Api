using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        private readonly IUserService _userService;
        private readonly IRecaptchaService _recaptcha;
        private readonly UserManager<DbUser> _userManager;
        private readonly ResourceManager _resourceManager;

        public AccountController(IUserService userService, 
            IRecaptchaService recaptcha, 
            UserManager<DbUser> userManager, 
            ResourceManager resourceManager)
        {
            _userService = userService;
            _recaptcha = recaptcha;
            _userManager = userManager;
            _resourceManager = resourceManager;
        }

        /// <summary>
        /// Login user into system.
        /// </summary>
        /// <returns>Object with user token and refresh token.</returns>
        /// <response code="200">Returns object with tokens.</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponseApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginApiModel model)
        {
            var validator = new LoginValidator(_userManager, _recaptcha, _resourceManager);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var loginResult = await _userService.LoginUser(model);
            return Ok(loginResult);
        }

        /// <summary>
        /// Registers a new user and logs in.
        /// </summary>
        /// <returns>Object with user token and refresh token.</returns>
        /// <response code="201">Returns object with tokens.</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="409">Conflict. Returns message with error or returns redirect response api model</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(TokenResponseApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(RedirectResponseApiModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserRegisterApiModel model)
        {
            var validator = new RegisterValidator(_userManager, _recaptcha, _resourceManager);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var RegisterResult = await _userService.RegisterUser(model);
            return Created("", RegisterResult);
        }

        /// <summary>
        /// Refresh tokens.
        /// </summary>
        /// <returns>Object with user token and refresh token.</returns>
        /// <response code="200">Returns object with tokens.</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("refreshtoken")]
        [ProducesResponseType(typeof(TokenResponseApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new MessageApiModel()
                {
                    Message = ModelState.Select(x => x.Value.Errors).FirstOrDefault().ToString()
                });
            }

            var resultRefreshToken = await _userService.RefreshToken(model);
            return Ok(resultRefreshToken);
        }
    }
}
