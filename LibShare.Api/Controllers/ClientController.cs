using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Resources;
using System.Threading.Tasks;

namespace LibShare.Api.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IUserService<UserApiModel> _userService;
        private readonly ResourceManager _resourceManager;

        public ClientController(IUserService<UserApiModel> userService,
            ResourceManager resourceManager)
        {
            _userService = userService;
            _resourceManager = resourceManager;
        }

        /// <summary>
        /// Get information about authorized user.
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">if get information about current user is correct.</response>
        /// <response code="400">if get information about current user is incorrect.</response>
        /// <response code="401">If user is unauthorized or token is bad/expired.</response>
        /// <response code="404">If user not found.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("info")]
        public async Task<IActionResult> GetInfoAboutAuthorizedUser()
        {
            var userId = User.FindFirst("id")?.Value;
            var result = await _userService.GetUserByIdWithFullPhotoUrlAsync(userId, Request);
            return Ok(result);
        }

        /// <summary>
        /// Get authorized user's photo.
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">if request is correct.</response>
        /// <response code="400">if request is incorrect.</response>
        /// <response code="401">If user is unauthorized or token is bad/expired.</response>
        /// <response code="404">If user not found.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(ImageApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("photo")]
        public async Task<IActionResult> GetCurrentUserPhoto()
        {
            var userId = User.FindFirst("id")?.Value;
            var result = await _userService.GetUserPhotoAsync(userId, Request);
            return Ok(result);
        }

        /// <summary>
        /// Sets authorized user photo. Size limit 100 MB. Format base64.
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">If set user photo request is correct.</response>
        /// <response code="400">If set user photo request is incorrect.</response>
        /// <response code="401">If user is unauthorized or token is bad/expired.</response>
        /// <response code="404">If user not found.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(ImageApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [RequestSizeLimit(100 * 1024 * 1024)]     // set the maximum file size limit to 100 MB
        [HttpPost("change-photo")]
        public async Task<IActionResult> ChangeUserPhoto([FromBody] ImageApiModel model)
        {
            ImageBase64Validator validator = new ImageBase64Validator(_resourceManager);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var userId = User.FindFirst("id")?.Value;
            var result = await _userService.UpdateUserPhotoAsync(model, userId, Request);
            return Ok(result);
        }

        /// <summary>
        /// Sets user profile (information about authorized user).
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">If the user profile successfully updated.</response>
        /// <response code="400">If the request to set the user profile is incorrect.</response>
        /// <response code="401">If user is unauthorized or token is bad/expired.</response>
        /// <response code="404">If user not found.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("set-profile")]
        public async Task<IActionResult> SetUserProfile([FromBody] UserApiModel model)
        {
            UserApiModelValidator validator = new UserApiModelValidator(_resourceManager);
            var validResult = validator.Validate(model);

            if (!validResult.IsValid)
            {
                return BadRequest(new MessageApiModel() { Message = validResult.ToString() });
            }

            var id = User.FindFirst("id")?.Value;
            model.Id = id;
            var result = await _userService.UpdateUserAsync(model);
            return Ok(result);
        }
    }
}
