using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces
{
    public interface ILibraryService
    {
        IEnumerable<CategoryApiModel> GetCategories();
        Task<BookApiModel> CreateBookAsync(BookApiModel book);
        Task<BookApiModel> GetBookByIdAsync(string bookId);
        Task<CategoryApiModel> GetCategoryByIdAsync(string categoryId);
        Task<BookApiModel> UpdateBookAsync(BookApiModel book);
        Task<MessageApiModel> DeleteBookAsync(string bookId);
        PagedListApiModel<BookApiModel> GetAllBooksSortPaginate();
        PagedListApiModel<BookApiModel> SearchSortPaginate(string searchString);
        PagedListApiModel<BookApiModel> FilterByMultiCategorySortPaginate(string[] categories);
        PagedListApiModel<BookApiModel> FilterByCategorySortPaginate(string categoryId);
        PagedListApiModel<BookApiModel> GetBooksByUserIdSortPaginate(string userId);
        Task<MessageApiModel> SendMessageToOwner(string userId, CallOwnerApiModel model);
    }
}