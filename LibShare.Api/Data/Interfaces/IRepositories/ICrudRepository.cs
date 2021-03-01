using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces.IRepositories
{
    public interface ICrudRepository<Type, TypeId> : IDisposable
    where Type : class
    {
        Task<IdentityResult> CreateAsync(Type item, string password);
        Task<bool> UpdateAsync(Type item);
        Task<Type> DeleteAsync(TypeId id, string deletionReason);
        Task<Type> GetByIdAsync(TypeId id);
        Task<Type> GetByEmailAsync(string email);
        Task<IEnumerable<Type>> GetAllAsync();
        Task<IEnumerable<Type>> FindAsync(Expression<Func<Type, bool>> predicate);
        Task<bool> UpdateUserTokenAsync(string userId, string refreshToken);
        Task<bool> UpdateUserPhotoAsync(Type user, string photo);
    }
}
