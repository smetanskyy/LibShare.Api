using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces
{
    public interface IUserService
    {
        // Task<ResponseModel<T>> GetUserById(long id);
        // Task<ResponseModel<IEnumerable<T>>> GetAllUsers();
        // Task<ResponseModel<IEnumerable<T>>> FindUser(Expression<Func<T, bool>> predicate);
        // Task<ResponseModel<T>> CreateUser(UserDTO userDTO);
        Task<TokenResponseApiModel> LoginUser(UserLoginApiModel model);
        Task<TokenResponseApiModel> RegisterUser(UserRegisterApiModel model);
        Task<TokenResponseApiModel> RefreshToken(TokenRequestApiModel tokenApiModel);

        //Task<bool> UpdateUser(T user);
        //Task<bool> DeleteUserById(long id);

        void Dispose();
    }
}
