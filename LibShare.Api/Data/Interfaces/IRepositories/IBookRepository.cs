using LibShare.Api.Data.Entities;

namespace LibShare.Api.Data.Interfaces.IRepositories
{
    interface IBookRepository : ICrudRepository<Book, string>
    {
    }
}
