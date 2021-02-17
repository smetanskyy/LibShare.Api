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
        Task<IdentityResult> Create(Type item, string password);
        Task<bool> Update(Type item);
        Task<bool> Delete(TypeId id);
        Task<Type> GetById(TypeId id);
        Task<Type> GetByEmail(string email);
        Task<IEnumerable<Type>> GetAll();
        Task<IEnumerable<Type>> Find(Expression<Func<Type, bool>> predicate);
        Task<bool> UpdateUserToken(Type user, string refreshToken);
    }
}
