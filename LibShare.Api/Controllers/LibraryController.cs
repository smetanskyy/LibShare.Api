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
        public IActionResult GetAllCategories()
        {
            var result = _libraryService.GetCategories();
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
        public IActionResult GetAllBooks([FromQuery] string pageSize, string pageNumber)
        {
            int size, page;
            ParseData(pageSize, out size, pageNumber, out page);

            var result = _libraryService.GetAllBooks(SortOrder.Title, size, page);
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
        /// Get books by using pagination and multi-filter (many categories and subcategories).
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Get books by using pagination</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(PagedListApiModel<BookApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("books-multifilter")]
        public IActionResult GetAllBooks([FromQuery] string[] chosenCategories, string pageSize, string pageNumber)
        {
            int size, page;
            ParseData(pageSize, out size, pageNumber, out page);

            var result = _libraryService.FilterByMultiCategorySortPaginate(chosenCategories, SortOrder.Title, size, page);
            return Ok(result);
        }

        /// <summary>
        /// Get books by using pagination and simple-filter (only one category and subcategories)
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Get books by using pagination</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(PagedListApiModel<BookApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("books-filter")]
        public IActionResult GetAllBooks([FromQuery] string chosenCategory, string pageSize, string pageNumber)
        {
            int size, page;
            ParseData(pageSize, out size, pageNumber, out page);

            var result = _libraryService.FilterByCategorySortPaginate(chosenCategory, SortOrder.Title, size, page);
            return Ok(result);
        }

        /// <summary>
        /// Search books by using pagination
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Get books by using pagination</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(PagedListApiModel<BookApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("books-search")]
        public IActionResult GetSearchBooks([FromQuery] string search, string pageSize, string pageNumber)
        {
            int size, page;
            ParseData(pageSize, out size, pageNumber, out page);

            var result = _libraryService.SearchSortPaginate(search, SortOrder.Title, size, page);
            return Ok(result);
        }

        /// <summary>
        /// Get book by id
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Get book by id</response>
        /// <response code="404">Book not found</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(BookApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("book")]
        public async Task<IActionResult> GetBookById([FromQuery] string bookId)
        {
            var result = await _libraryService.GetBookByIdAsync(bookId);
            return Ok(result);
        }

        /// <summary>
        /// Update book
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Update book by id</response>
        /// <response code="404">Book not found</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("book-update")]
        public async Task<IActionResult> UpdateBook([FromBody] BookApiModel model)
        {
            var result = await _libraryService.UpdateBookAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Delete book by id
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Delete book by id</response>
        /// <response code="404">Book not found</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(BookApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("book-delete")]
        public async Task<IActionResult> DeleteBookById([FromQuery] string bookId)
        {
            var result = await _libraryService.DeleteBookAsync(bookId);
            return Ok(result);
        }

        private void ParseData(string pageSize, out int size, string pageNumber, out int page)
        {
            int.TryParse(pageSize, out size);
            size = size < 1 ? 10 : size;

            int.TryParse(pageNumber, out page);
            page = page < 1 ? 1 : page;
        }
    }
}
