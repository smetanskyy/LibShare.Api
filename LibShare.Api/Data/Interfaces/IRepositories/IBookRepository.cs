using LibShare.Api.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces.IRepositories
{
    public interface IBookRepository : ICrudRepository<Book, string>
    {
        IEnumerable<Book> Sort(IEnumerable<Book> books, SortOrder sortOrder);
        IEnumerable<Book> Search(string searchString);
        List<Book> Paginate(IEnumerable<Book> books, int pageSize = 10, int page = 1);
        IEnumerable<Book> FilterByMultiCategory(string[] categories);
        IEnumerable<Book> FilterByCategory(string categoryId);
    }

    public enum SortOrder
    {
        Id,
        Title,
        Author,
        Publisher,
        Year,
        Language,
        Description,
        Category,
        User,
        DateCreate
    }
}
