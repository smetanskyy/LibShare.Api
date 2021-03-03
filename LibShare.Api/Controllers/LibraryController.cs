using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Api.Controllers
{
    [Route("api/library")]
    [ApiController]
    [Produces("application/json")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="200">Get all categories</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(IEnumerable<CategoryApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _libraryService.GetCategoriesAsync();
            return Ok(result);
        }
    }
}
