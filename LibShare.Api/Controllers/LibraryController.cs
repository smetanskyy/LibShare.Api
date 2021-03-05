using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Data.Interfaces.IRepositories;
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
        /// <returns>Returns object with data</returns>
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

        /// <summary>
        /// Get all books by using pagination.
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Get books by using pagination</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(PagedListApiModel<BookApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooks([FromQuery] string pageSize, string pageNumber)
        {
            int size;
            int page;
            try
            {
               size = int.Parse(pageSize);
               page = int.Parse(pageNumber);
            }
            catch (Exception)
            {
                size = 10;
                page = 1;
            }
            var result = await _libraryService.GetAllBooksAsync(SortOrder.Title, size, page);
            return Ok(result);
        }

        /// <summary>
        /// Add new book.
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Book has added successfully</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(BookApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody] BookApiModel model)
        {
            var result = await _libraryService.CreateBookAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Get books by using pagination and multi-filter.
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Get books by using pagination</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(PagedListApiModel<BookApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("books-multifilter")]
        public IActionResult GetAllBooks([FromQuery] string[] categories, string pageSize, string pageNumber)
        {
            int size;
            int page;
            try
            {
                size = int.Parse(pageSize);
                page = int.Parse(pageNumber);
            }
            catch (Exception)
            {
                size = 10;
                page = 1;
            }
            var result = _libraryService.FilterByMultiCategoryPaginateSort(categories, SortOrder.Title, size, page);
            return Ok(result);
        }
    }
}
