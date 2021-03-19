using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces
{
    public interface IAccountService<T> where T : class
    {
        Task<T> LoginUserAsync(UserLoginApiModel model);
        Task<MessageApiModel> LogoutUserAsync(string userId);
        Task<T> RegisterUserAsync(UserRegisterApiModel model);
        Task<T> RefreshTokenAsync(TokenApiModel tokenApiModel);
        Task<MessageApiModel> RestorePasswordSendLinkOnEmailAsync(string userEmail);
        Task<T> RestorePasswordBaseAsync(RestoreApiModel model);
        Task<T> ChangeUserPasswordAsync(ChangePasswordApiModel model, string userId);
        Task<MessageApiModel> ConfirmMailBaseAsync(ConfirmApiModel model);
        Task<MessageApiModel> ConfirmMailSendLinkOnEmailAsync(string userEmail);
        Task<MessageApiModel> DeleteUserByIdAsync(string userId);
    }
}
