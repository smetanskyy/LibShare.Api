using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Constants;
using LibShare.Api.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace LibShare.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IUserService<UserApiModel> _userService;
        private readonly ResourceManager _resourceManager;

        public AdminController(IUserService<UserApiModel> userService,
            ResourceManager resourceManager)
        {
            _userService = userService;
            _resourceManager = resourceManager;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of users</returns>
        /// <response code="200">Returns a list of users</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="404">If there are no users</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(IEnumerable<UserApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("all-users")]
        public IActionResult GetAllUsersAsync()
        {
            var result = _userService.GetAllUsers();
            return Ok(result);
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <returns>Returns user</returns>
        /// <response code="200">Returns a user</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="404">If user not found</response>
        /// <response code="500">Internal server error.</response>
        /// <param name="id" example="01f75261-2feb-4a34-93fb-ab26bf16cbe7">User ID</param>
        [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            Guid guid;
            try
            {
                guid = Guid.Parse(id);
            }
            catch (Exception)
            {
                return BadRequest("Wrong Id: " + id);
            }
            var result = await _userService.GetUserByIdWithFullPhotoUrlAsync(guid.ToString("D"), Request);
            return Ok(result);
        }


        /// <summary>
        /// Update user.
        /// </summary>
        /// <returns>Returns user</returns>
        /// <response code="200">Returns a user</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="404">If user not found</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("user-update")]
        public async Task<IActionResult> UserUpdateAsync([FromBody] UserApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest( new MessageApiModel() { 
                Message = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                } );
            }

            var result = await _userService.UpdateUserAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Create user.
        /// </summary>
        /// <returns>Returns user</returns>
        /// <response code="200">Returns a user</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="404">If user not found</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("user-create")]
        public async Task<IActionResult> UserCreateAsync([FromBody] UserApiModel model, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new MessageApiModel()
                {
                    Message = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                });
            }

            var result = await _userService.CreateUserAsync(model, password);
            return Ok(result);
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <returns>Returns user</returns>
        /// <response code="200">Delete user</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="404">If user not found</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("user-delete")]
        public async Task<IActionResult> UserCreateAsync([FromBody] string userId, string deleteReason)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new MessageApiModel()
                {
                    Message = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                });
            }

            var result = await _userService.DeleteUserByIdAsync(userId, deleteReason);
            return Ok(result);
        }

        /// <summary>
        /// Change user photo. Size limit 100 MB. Format base64.
        /// </summary>
        /// <returns>Returns new url</returns>
        /// <response code="200">Returns new url</response>
        /// <response code="400">Bad request. Returns message with error.</response>
        /// <response code="404">If user not found</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(ImageApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [RequestSizeLimit(100 * 1024 * 1024)]     // set the maximum file size limit to 100 MB
        [HttpPost("user-update-photo")]
        public async Task<IActionResult> UserCreateAsync([FromBody] ImageApiModel image, string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new MessageApiModel()
                {
                    Message = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                });
            }

            var result = await _userService.UpdateUserPhotoAsync(image, userId, Request);
            return Ok(result);
        }
    }
}
