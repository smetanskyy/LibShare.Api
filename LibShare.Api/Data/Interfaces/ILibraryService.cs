using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Interfaces.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces
{
    public interface ILibraryService
    {
        IEnumerable<CategoryApiModel> GetCategories();
        Task<BookApiModel> CreateBookAsync(BookApiModel book);
        Task<BookApiModel> GetBookByIdAsync(string bookId);
        Task<bool> UpdateBookAsync(BookApiModel book);
        Task<BookApiModel> DeleteBookAsync(string bookId, string deletionReason);
        Task<PagedListApiModel<BookApiModel>> GetAllBooksAsync(SortOrder sortOrder, int pageSize = 10, int page = 1);
        PagedListApiModel<BookApiModel> SearchPaginateSort(string searchString, SortOrder sortOrder, int pageSize = 10, int page = 1);
        PagedListApiModel<BookApiModel> FilterByMultiCategoryPaginateSort(string[] categories, SortOrder sortOrder, int pageSize = 10, int page = 1);
        PagedListApiModel<BookApiModel> FilterByCategoryPaginateSort(string categoryIdint, SortOrder sortOrder, int pageSize = 10, int page = 1);
    }
}
