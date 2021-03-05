using LibShare.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces.IRepositories
{
    public interface ICategoryRepository : ICrudRepository<Category, string> 
    {
        IEnumerable<Category> GetAll();
    }
}
