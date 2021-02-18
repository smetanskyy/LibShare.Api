using LibShare.Api.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces
{
    public interface IUserService<T> where T : class
    {
        Task<T> GetUserByIdAsync(string id);
        Task<IEnumerable<T>> GetAllUsersAsync();
        Task<IEnumerable<T>> FindUserAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateUserAsync(UserDTO userDTO);
        Task<bool> UpdateUserAsync(T user);
        Task<bool> DeleteUserByIdAsync(string id);
    }
}
