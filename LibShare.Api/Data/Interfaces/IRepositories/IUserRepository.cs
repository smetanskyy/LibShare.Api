using LibShare.Api.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces.IRepositories
{
    public interface IUserRepository : ICrudRepository<DbUser, string>
    {
        Task<IdentityResult> CreateAsync(DbUser item, string password);
        Task<DbUser> GetByEmailAsync(string email);
        Task<bool> UpdateUserTokenAsync(string userId, string refreshToken);
        Task<bool> UpdateUserPhotoAsync(DbUser user, string photo);
        Task<bool> CreateProfileAsync(string userId);
    }
}
