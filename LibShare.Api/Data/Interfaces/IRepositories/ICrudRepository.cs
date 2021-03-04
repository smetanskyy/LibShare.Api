using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces.IRepositories
{
    public interface ICrudRepository<Type, TypeId> : IDisposable
    where Type : class
    {
        Task<Type> CreateAsync(Type item);
        Task<Type> DeleteAsync(TypeId id, string deletionReason);
        Task<Type> GetByIdAsync(TypeId id);
        Task<IEnumerable<Type>> GetAllAsync();
        Task<IEnumerable<Type>> FindAsync(Expression<Func<Type, bool>> predicate);
        Task<bool> UpdateAsync(Type item);
    }
}
