using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Services
{
    public class UserService : IUserService
    {
        private readonly ICrudRepository<DbUser, long> _userRepository;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly IJwtService _jwtService;
        public UserService(ICrudRepository<DbUser, long> userRepository,
            UserManager<DbUser> userManager, SignInManager<DbUser> signInManager,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
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
                tokenResponse.ErrorMessage = "Login or password is incorrect!";
                return tokenResponse;
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!loginResult.Succeeded)
            {
                tokenResponse.ErrorMessage = "Login or password is incorrect!";
                return tokenResponse;
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
            var tokenResponse = new TokenResponseApiModel();

            string accessToken = tokenApiModel.Token;
            string refreshToken = tokenApiModel.RefreshToken;

            var claims = _jwtService.GetClaimsFromExpiredToken(accessToken);
            if (claims == null)
            {
                tokenResponse.ErrorMessage = "Invalid client request!";
                return tokenResponse;
            }

            var userId = claims.First(claim => claim.Type == "id").Value;
            var user = await _userManager.Users.Include(u => u.Token).SingleAsync(x => x.Id == long.Parse(userId));

            if (user == null || user.Token == null || user.Token.RefreshToken != refreshToken || user.Token.RefreshTokenExpiryTime <= DateTime.Now)
            {
                tokenResponse.ErrorMessage = "Invalid client request!";
                return tokenResponse;
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
                tokenResponse.ErrorMessage = "User already exist!";
                return tokenResponse;
            }

            if (!model.Password.Equals(model.ConfirmPassword))
            {
                tokenResponse.ErrorMessage = "Password and confirm password don't match!";
                return tokenResponse;
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
                tokenResponse.ErrorMessage = resultCreated.Errors.First().Description;
                return tokenResponse;
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
