using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Infrastructure.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Services
{
    public class UserService : IUserService
    {
        private readonly ICrudRepository<DbUser, string> _userRepository;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly ResourceManager _resourceManager;

        public UserService(ICrudRepository<DbUser, string> userRepository,
            UserManager<DbUser> userManager, 
            SignInManager<DbUser> signInManager,
            IJwtService jwtService,
            ResourceManager resourceManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _resourceManager = resourceManager;
        }

        public void Dispose()
        {
            _userRepository.Dispose();
        }

        public async Task<TokenResponseApiModel> LoginUser(UserLoginApiModel model)
        {
            var tokenResponse = new TokenResponseApiModel();
            var user = await _userRepository.GetByEmail(model.Email);

            if (user == null)
            {
                throw new BadRequestException(_resourceManager.GetString("LoginOrPasswordInvalid"));
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!loginResult.Succeeded)
            {
                throw new BadRequestException(_resourceManager.GetString("LoginOrPasswordInvalid"));
            }

            var token = _jwtService.CreateToken(_jwtService.SetClaims(user));
            var refreshToken = _jwtService.CreateRefreshToken();

            await _userRepository.UpdateUserToken(user, refreshToken);
            await _signInManager.SignInAsync(user, isPersistent: false);

            tokenResponse.Token = token;
            tokenResponse.RefreshToken = refreshToken;

            return tokenResponse;
        }

        public async Task<TokenResponseApiModel> RefreshToken(TokenRequestApiModel tokenApiModel)
        {
            if(tokenApiModel.Token == null || tokenApiModel.RefreshToken == null)
                throw new ArgumentNullException(_resourceManager.GetString("ArgumentNullException"));

            var tokenResponse = new TokenResponseApiModel();

            string accessToken = tokenApiModel.Token;
            string refreshToken = tokenApiModel.RefreshToken;

            var claims = _jwtService.GetClaimsFromExpiredToken(accessToken);

            if (claims == null)
            {
                throw new BadRequestException(_resourceManager.GetString("InvalidClientRequest"));
            }

            var userId = claims.First(claim => claim.Type == "id").Value;
            var user = await _userManager.Users.Include(u => u.Token).SingleAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new BadRequestException(_resourceManager.GetString("UserDoesNotExist"));
            }

            if (user.Token == null || user.Token.RefreshToken != refreshToken)
            {
                throw new BadRequestException(_resourceManager.GetString("YouMustLogInFirst"));
            }

            if (user.Token.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new BadRequestException(_resourceManager.GetString("RefreshTokenExpired"));
            }

            var newAccessToken = _jwtService.CreateToken(claims);
            var newRefreshToken = _jwtService.CreateRefreshToken();

            await _userRepository.UpdateUserToken(user, newRefreshToken);

            tokenResponse.Token = newAccessToken;
            tokenResponse.RefreshToken = newRefreshToken;
            return tokenResponse;
        }

        public async Task<TokenResponseApiModel> RegisterUser(UserRegisterApiModel model)
        {
            var tokenResponse = new TokenResponseApiModel();

            var searchUser = _userManager.FindByEmailAsync(model.Email);

            if (searchUser.Result != null)
            {
                throw new BadRequestException(_resourceManager.GetString("UserAlreadyExists"));
            }

            if (!model.Password.Equals(model.ConfirmPassword))
            {
                throw new BadRequestException(_resourceManager.GetString("PasswordsNotMatch"));
            }

            var dbUser = new DbUser
            {
                Email = model.Email,
                UserName = model.Username
            };

            var resultCreated = await _userRepository.Create(dbUser, model.Password);

            if (resultCreated == null) return tokenResponse;
            if (!resultCreated.Succeeded)
            {
                throw new BadRequestException(resultCreated.Errors.First().Description);
            }

            var token = _jwtService.CreateToken(_jwtService.SetClaims(dbUser));
            var refreshToken = _jwtService.CreateRefreshToken();

            await _userRepository.UpdateUserToken(dbUser, refreshToken);
            await _signInManager.SignInAsync(dbUser, isPersistent: false);

            tokenResponse.Token = token;
            tokenResponse.RefreshToken = refreshToken;

            return tokenResponse;
        }
    }
}
