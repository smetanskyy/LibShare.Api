using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        /// Get category by id
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Get category by id</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(CategoryApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("category")]
        public async Task<IActionResult> GetCategoryById([FromQuery] string categoryId)
        {
            var result = await _libraryService.GetCategoryByIdAsync(categoryId);
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
        public IActionResult GetAllBooks([FromQuery] string pageSize, string pageNumber, 
            bool onlyEbooks = false, bool onlyRealBooks = false, SortOrder sortOrder = SortOrder.Title)
        {
            int size, page;
            ParseData(pageSize, out size, pageNumber, out page);

            BookParameters.PageSize = size;
            BookParameters.PageNumber = page;
            BookParameters.OnlyEbooks = onlyEbooks;
            BookParameters.OnlyRealBooks = onlyRealBooks;
            BookParameters.SortOrder = sortOrder;

            var result = _libraryService.GetAllBooksSortPaginate();
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
        [Authorize]
        public async Task<IActionResult> AddBook([FromBody] BookApiModel model)
        {
            model.Image = null;
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
        public IActionResult GetBooksByMultiFilter([FromQuery] string[] chosenCategories, string pageSize, 
            string pageNumber, bool onlyEbooks = false, bool onlyRealBooks = false, SortOrder sortOrder = SortOrder.Title)
        {
            int size, page;
            ParseData(pageSize, out size, pageNumber, out page);

            BookParameters.PageSize = size;
            BookParameters.PageNumber = page;
            BookParameters.OnlyEbooks = onlyEbooks;
            BookParameters.OnlyRealBooks = onlyRealBooks;
            BookParameters.SortOrder = sortOrder;


            var result = _libraryService.FilterByMultiCategorySortPaginate(chosenCategories);
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
        public IActionResult GetBooksByFilter([FromQuery] string chosenCategory, string pageSize, string pageNumber,
            bool onlyEbooks = false, bool onlyRealBooks = false, SortOrder sortOrder = SortOrder.Title)
        {
            int size, page;
            ParseData(pageSize, out size, pageNumber, out page);

            BookParameters.PageSize = size;
            BookParameters.PageNumber = page;
            BookParameters.OnlyEbooks = onlyEbooks;
            BookParameters.OnlyRealBooks = onlyRealBooks;
            BookParameters.SortOrder = sortOrder;


            var result = _libraryService.FilterByCategorySortPaginate(chosenCategory);
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
        public IActionResult GetSearchBooks([FromQuery] string search, string pageSize, string pageNumber,
            bool onlyEbooks = false, bool onlyRealBooks = false, SortOrder sortOrder = SortOrder.Title)
        {
            int size, page;
            ParseData(pageSize, out size, pageNumber, out page);

            BookParameters.PageSize = size;
            BookParameters.PageNumber = page;
            BookParameters.OnlyEbooks = onlyEbooks;
            BookParameters.OnlyRealBooks = onlyRealBooks;
            BookParameters.SortOrder = sortOrder;

            var result = _libraryService.SearchSortPaginate(search);
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
        [ProducesResponseType(typeof(BookApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("book-update")]
        [Authorize]
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
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageApiModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("book-delete")]
        [Authorize]
        public async Task<IActionResult> DeleteBookById([FromQuery] string bookId)
        {
            var result = await _libraryService.DeleteBookAsync(bookId);
            return Ok(result);
        }

        /// <summary>
        /// Get users books by using pagination
        /// </summary>
        /// <returns>Returns object with data</returns>
        /// <response code="200">Get books by using pagination</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(typeof(PagedListApiModel<BookApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("books-user")]
        [Authorize]
        public IActionResult GetBooksByUserId(string pageSize, string pageNumber, bool onlyEbooks = false,
            bool onlyRealBooks = false, SortOrder sortOrder = SortOrder.Title)
        {
            int size, page;
            ParseData(pageSize, out size, pageNumber, out page);

            BookParameters.PageSize = size;
            BookParameters.PageNumber = page;
            BookParameters.OnlyEbooks = onlyEbooks;
            BookParameters.SortOrder = sortOrder;
            BookParameters.OnlyRealBooks = onlyRealBooks;

            var userId = User.FindFirst("id")?.Value;
            var result = _libraryService.GetBooksByUserIdSortPaginate(userId);
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
