using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Resources;
using System.Threading.Tasks;

namespace LibShare.Api.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Produces("application/json")]
    public class ClientController : ControllerBase
    {
        private readonly IUserService<UserApiModel> _userService;
        private readonly IRecaptchaService _recaptcha;
        private readonly ResourceManager _resourceManager;

        public ClientController(IUserService<UserApiModel> userService,
            IRecaptchaService recaptcha,
            ResourceManager resourceManager)
        {
            _userService = userService;
            _recaptcha = recaptcha;
            _resourceManager = resourceManager;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of users</returns>
        /// <response code="200">Returns a list of users</response>
        /// <response code="404">If there are no users</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserApiModel>), 200)]
        [ProducesResponseType(typeof(MessageApiModel), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }
    }
}
