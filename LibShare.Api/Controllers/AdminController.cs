using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Threading.Tasks;

namespace LibShare.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Produces("application/json")]
    //[Authorize(Roles = Roles.Admin)]
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
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();
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
            Guid guid = Guid.Parse(id);
            var result = await _userService.GetUserByIdAsync(guid.ToString("D"));
            return Ok(result);
        }
    }
}
