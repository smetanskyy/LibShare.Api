using LibShare.Api.Data.ApiModels.ResponseApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces
{
    public interface ILibraryService
    {
        Task<IEnumerable<CategoryApiModel>> GetCategoriesAsync();
    }
}
