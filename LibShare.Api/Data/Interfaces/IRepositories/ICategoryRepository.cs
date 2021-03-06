using LibShare.Api.Data.Entities;
using System.Collections.Generic;

namespace LibShare.Api.Data.Interfaces.IRepositories
{
    public interface ICategoryRepository : ICrudRepository<Category, string>
    {
        string[] GetAllSubCategoriesIdFromFilter(string[] chosenCategories);
    }
}
