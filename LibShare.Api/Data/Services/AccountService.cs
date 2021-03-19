using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Constants;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Data.Interfaces.IRepositories;
using LibShare.Api.Infrastructure.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Services
{
    public class AccountService : IAccountService<TokenApiModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly ResourceManager _resourceManager;
        private readonly IEmailService _emailService;

        public AccountService(IUserRepository userRepository,
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager,
            IJwtService jwtService,
            ResourceManager resourceManager,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _resourceManager = resourceManager;
            _emailService = emailService;
        }

        public async Task<TokenApiModel> LoginUserAsync(UserLoginApiModel model)
        {
            var user = _userManager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                throw new BadRequestException(_resourceManager.GetString("LoginOrPasswordInvalid"));
            }

            if (user != null && user.IsDeleted == true)
            {
                throw new UserIsDeletedException(_resourceManager.GetString("UserIsDeleted"));
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!loginResult.Succeeded)
            {
                throw new BadRequestException(_resourceManager.GetString("LoginOrPasswordInvalid"));
            }

            var token = _jwtService.CreateToken(_jwtService.SetClaims(user));
            var refreshToken = _jwtService.CreateRefreshToken();

            await _userRepository.UpdateUserTokenAsync(user.Id, refreshToken);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return new TokenApiModel { Token = token, RefreshToken = refreshToken };
        }

        public async Task<TokenApiModel> RefreshTokenAsync(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel.Token == null || tokenApiModel.RefreshToken == null)
                throw new ArgumentNullException(_resourceManager.GetString("ArgumentNullException"));

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

            await _userRepository.UpdateUserTokenAsync(userId, newRefreshToken);

            return new TokenApiModel { Token = newAccessToken, RefreshToken = newRefreshToken };
        }

        public async Task<TokenApiModel> RegisterUserAsync(UserRegisterApiModel model)
        {
            var searchUser = _userManager.FindByEmailAsync(model.Email).Result;

            if (searchUser != null && searchUser.IsDeleted == true)
            {
                throw new UserIsDeletedException(_resourceManager.GetString("UserIsDeleted"));
            }

            if (searchUser != null)
            {
                throw new BadRequestException(_resourceManager.GetString("EmailExist"));
            }

            if (!model.Password.Equals(model.ConfirmPassword))
            {
                throw new BadRequestException(_resourceManager.GetString("PasswordsNotMatch"));
            }

            var userByUsername = _userManager.FindByNameAsync(model.UserName).Result;
            if (userByUsername != null)
            {
                throw new BadRequestException(_resourceManager.GetString("UsernameExist"));
            }

            var dbUser = new DbUser
            {
                Email = model.Email,
                UserName = model.UserName,
            };

            var resultCreated = await _userRepository.CreateAsync(dbUser, model.Password);

            if (!resultCreated.Succeeded)
            {
                throw new BadRequestException(resultCreated.Errors.First().Description);
            }

            await _userRepository.CreateProfileAsync(dbUser.Id);

            var token = _jwtService.CreateToken(_jwtService.SetClaims(dbUser));
            var refreshToken = _jwtService.CreateRefreshToken();

            await _userRepository.UpdateUserTokenAsync(dbUser.Id, refreshToken);
            await _signInManager.SignInAsync(dbUser, isPersistent: false);

            return new TokenApiModel { Token = token, RefreshToken = refreshToken };
        }

        public async Task<MessageApiModel> RestorePasswordSendLinkOnEmailAsync(string userEmail, HttpRequest request)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                throw new BadRequestException(_resourceManager.GetString("UserDoesNotExist"));
            }

            if (user != null && user.IsDeleted == true)
            {
                throw new UserIsDeletedException(_resourceManager.GetString("UserIsDeleted"));
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var serverUrl = $"{request.Scheme}://{request.Host}/";
            var url = serverUrl + $"restore?email={user.Email}&token={token}";

            var topic = _resourceManager.GetString("RestorePassword");

            var html = HtmlStrings.GetHtmlEmailForRestorePassword(url);

            await _emailService.SendAsync(userEmail, topic, html);

            return new MessageApiModel { Message = _resourceManager.GetString("RestoreInstruction") };
        }

        public async Task<TokenApiModel> RestorePasswordBaseAsync(RestoreApiModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new BadRequestException(_resourceManager.GetString("UserDoesNotExist"));
            }

            if (user != null && user.IsDeleted == true)
            {
                throw new UserIsDeletedException(_resourceManager.GetString("UserIsDeleted"));
            }

            var restoreResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!restoreResult.Succeeded)
            {
                throw new BadRequestException(restoreResult.Errors.First().Description);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, model.NewPassword, false, false);

            if (!loginResult.Succeeded)
            {
                throw new BadRequestException(_resourceManager.GetString("LoginOrPasswordInvalid"));
            }

            var token = _jwtService.CreateToken(_jwtService.SetClaims(user));
            var refreshToken = _jwtService.CreateRefreshToken();

            await _userRepository.UpdateUserTokenAsync(user.Id, refreshToken);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return new TokenApiModel { Token = token, RefreshToken = refreshToken };
        }

        public async Task<TokenApiModel> ChangeUserPasswordAsync(ChangePasswordApiModel model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new BadRequestException(_resourceManager.GetString("UserDoesNotExist"));
            }

            var changeResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changeResult.Succeeded)
            {
                throw new BadRequestException(_resourceManager.GetString("PasswordOldInvalid"));
            }

            var token = _jwtService.CreateToken(_jwtService.SetClaims(user));
            var refreshToken = _jwtService.CreateRefreshToken();

            await _userRepository.UpdateUserTokenAsync(userId, refreshToken);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return new TokenApiModel { Token = token, RefreshToken = refreshToken };
        }

        public async Task<MessageApiModel> ConfirmMailSendLinkOnEmailAsync(string userEmail, HttpRequest request)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                throw new BadRequestException(_resourceManager.GetString("UserDoesNotExist"));
            }

            if (user != null && user.IsDeleted == true)
            {
                throw new UserIsDeletedException(_resourceManager.GetString("UserIsDeleted"));
            }

            if (user.EmailConfirmed)
            {
                throw new ArgumentException(_resourceManager.GetString("EmailAlreadyConfirmed"));
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var serverUrl = $"{request.Scheme}://{request.Host}/";
            var url = serverUrl + $"confirm?email={user.Email}&token={token}";

            var topic = _resourceManager.GetString("ConfirmEmail");

            var html = HtmlStrings.GetHtmlEmailForConfirmAccount(url);

            await _emailService.SendAsync(userEmail, topic, html);

            return new MessageApiModel { Message = _resourceManager.GetString("ConfirmInstruction") };
        }

        public async Task<MessageApiModel> ConfirmMailBaseAsync(ConfirmApiModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new BadRequestException(_resourceManager.GetString("UserDoesNotExist"));
            }

            if (user != null && user.IsDeleted == true)
            {
                throw new UserIsDeletedException(_resourceManager.GetString("UserIsDeleted"));
            }

            if (user.EmailConfirmed)
            {
                throw new ArgumentException(_resourceManager.GetString("EmailAlreadyConfirmed"));
            }

            var confirmResult = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (!confirmResult.Succeeded)
            {
                throw new BadRequestException(confirmResult.Errors.First().Description);
            }

            return new MessageApiModel { Message = _resourceManager.GetString("EmailConfirmSucceeded") };
        }

        public async Task<MessageApiModel> DeleteUserByIdAsync(string userId)
        {
            await _userRepository.DeleteAsync(userId, _resourceManager.GetString("ClientDecision"));
             await LogoutUserAsync(userId);
            return new MessageApiModel { Message = _resourceManager.GetString("AccountDeleted") };
        }

        public async Task<MessageApiModel> LogoutUserAsync(string userId)
        {
            try
            {
                await _signInManager.SignOutAsync();
                await _userRepository.UpdateUserTokenAsync(userId, null);
                return new MessageApiModel { Message = _resourceManager.GetString("LogoutUser") };
            }
            catch (Exception)
            {
                return new MessageApiModel { Message = _resourceManager.GetString("ErrorUnknown") };
            }
        }
    }
}
