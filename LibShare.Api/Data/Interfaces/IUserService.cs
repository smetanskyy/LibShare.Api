using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces
{
    public interface IUserService
    {
        // Task<ResponseModel<T>> GetUserById(string id);
        // Task<ResponseModel<IEnumerable<T>>> GetAllUsers();
        // Task<ResponseModel<IEnumerable<T>>> FindUser(Expression<Func<T, bool>> predicate);
        // Task<ResponseModel<T>> CreateUser(UserDTO userDTO);

        Task<TokenResponseApiModel> LoginUser(UserLoginApiModel model);
        Task<TokenResponseApiModel> RegisterUser(UserRegisterApiModel model);
        Task<TokenResponseApiModel> RefreshToken(TokenRequestApiModel tokenApiModel);
        Task<MessageApiModel> RestorePasswordSendLinkOnEmail(string userEmail, HttpRequest request);
        Task<TokenResponseApiModel> RestorePasswordBase(RestoreApiModel model);
        Task<TokenResponseApiModel> ChangeUserPassword(ChangePasswordApiModel model, string userId);

        //Task<bool> UpdateUser(T user);
        //Task<bool> DeleteUserById(string id);

        void Dispose();
    }
}
