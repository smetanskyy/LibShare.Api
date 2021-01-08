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
using System.Net.Mime;
using System.Threading.Tasks;

namespace LibShare.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRecaptchaService _recaptcha;
        private readonly UserManager<DbUser> _userManager;

        public AccountController(IUserService userService, IRecaptchaService recaptcha, UserManager<DbUser> userManager)
        {
            _userService = userService;
            _recaptcha = recaptcha;
            _userManager = userManager;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UserLoginApiModel model)
        {
            var validator = new LoginValidator(_userManager, _recaptcha);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new TokenResponseApiModel() { ErrorMessage = validResult.ToString() });
            }

            var loginResult = await _userService.LoginUser(model);
            return string.IsNullOrEmpty(loginResult.ErrorMessage) ? Ok(loginResult) : BadRequest(loginResult);
        }

        // [RequestSizeLimit(100 * 1024 * 1024)]     // set the maximum file size limit to 100 MB

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserRegisterApiModel model)
        {
            var validator = new RegisterValidator(_userManager, _recaptcha);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new TokenResponseApiModel() { ErrorMessage = validResult.ToString() });
            }

            var RegisterResult = await _userService.RegisterUser(model);
            return string.IsNullOrEmpty(RegisterResult.ErrorMessage) ? Created("", RegisterResult) : BadRequest(RegisterResult);
        }

        [HttpPost("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new TokenResponseApiModel()
                {
                    ErrorMessage = ModelState.Select(x => x.Value.Errors).FirstOrDefault().ToString()
                });
            }

            var resultRefreshToken = await _userService.RefreshToken(model);
            return string.IsNullOrEmpty(resultRefreshToken.ErrorMessage) ? Ok(resultRefreshToken) : BadRequest(resultRefreshToken);
        }

        [HttpGet("test")]
        public ActionResult<IEnumerable<string>> GetUsers()
        {
            return new string[] { "stepan", "oleg" };
        }
    }
}
