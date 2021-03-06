using LibShare.Api.Data.ApiModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces
{
    public interface IUserService<T> where T : class
    {
        Task<T> GetUserByIdAsync(string id);
        Task<T> GetUserByIdWithFullPhotoUrlAsync(string userId, HttpRequest request);
        Task<ImageApiModel> GetUserPhotoAsync(string userId, HttpRequest request);
        Task<ImageApiModel> UpdateUserPhotoAsync(ImageApiModel model, string userId, HttpRequest request);
        IEnumerable<T> GetAllUsers();
        Task<IEnumerable<T>> FindUserAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateUserAsync(UserApiModel model, string password);
        Task<T> DeleteUserByIdAsync(string id, string deletionReason);
        Task<T> UpdateUserAsync(UserApiModel model);
    }
}
