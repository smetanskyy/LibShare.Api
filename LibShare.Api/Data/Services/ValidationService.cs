using FluentValidation;
using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Resources;

namespace LibShare.Api.Data.Services
{

    public class LoginValidator : AbstractValidator<UserLoginApiModel>
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly IRecaptchaService _recaptcha;
        private readonly ResourceManager _resourceManager;

        private DbUser _user;

        public LoginValidator(UserManager<DbUser> userManager, IRecaptchaService recaptcha, ResourceManager resourceManager)
        {
            _userManager = userManager;
            _recaptcha = recaptcha;
            _resourceManager = resourceManager;

            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.RecaptchaToken)
                .NotNull().WithMessage(_resourceManager.GetString("RecaptchaRequired"))
                .Must(_recaptcha.IsValid).WithMessage(_resourceManager.GetString("RecaptchaInvalid"));

            RuleFor(x => x.Email)
                .NotNull().WithMessage(_resourceManager.GetString("EmailRequired"))
                .EmailAddress().WithMessage(_resourceManager.GetString("EmailInvalid"));

            RuleFor(x => x.Password)
                .NotNull().WithMessage(_resourceManager.GetString("PasswordRequired"))
                .Length(8, 20).WithMessage(_resourceManager.GetString("PasswordLength"))
                .Matches(@"[A-Z]+").WithMessage(_resourceManager.GetString("PasswordUppercase"))
                .Matches(@"[a-z]+").WithMessage(_resourceManager.GetString("PasswordLowercase"))
                .Matches(@"[0-9]+").WithMessage(_resourceManager.GetString("PasswordNumber"))
                .Matches(@"[\W_]+").WithMessage(_resourceManager.GetString("PasswordW"));

            RuleFor(x => x.Email).Must(IsEmailExist).WithMessage(_resourceManager.GetString("LoginOrPasswordInvalid"));
            RuleFor(x => x.Password).Must(IsPasswordCorrect).WithMessage(_resourceManager.GetString("LoginOrPasswordInvalid"));
        }

        private bool IsEmailExist(string email)
        {
            _user = _userManager.FindByEmailAsync(email).Result;
            return _user != null ? true : false;
        }

        private bool IsPasswordCorrect(string password)
        {
            return _userManager.CheckPasswordAsync(_user, password).Result;
        }
    }

    public class RegisterValidator : AbstractValidator<UserRegisterApiModel>
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly IRecaptchaService _recaptcha;
        private readonly ResourceManager _resourceManager;

        public RegisterValidator(UserManager<DbUser> userManager, IRecaptchaService recaptcha, ResourceManager resourceManager)
        {
            _userManager = userManager;
            _recaptcha = recaptcha;
            _resourceManager = resourceManager;


            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.RecaptchaToken)
                .NotNull().WithMessage(_resourceManager.GetString("RecaptchaRequired"))
                .Must(_recaptcha.IsValid).WithMessage(_resourceManager.GetString("RecaptchaInvalid"));

            RuleFor(x => x.Email).NotNull().WithMessage(_resourceManager.GetString("EmailRequired"))
                .EmailAddress().WithMessage(_resourceManager.GetString("EmailInvalid"));

            RuleFor(x => x.Username).NotNull().WithMessage(_resourceManager.GetString("UsernameRequired"))
                .Length(2, 20).WithMessage(_resourceManager.GetString("UsernameLength"));

            RuleFor(x => x.Password).NotNull().WithMessage(_resourceManager.GetString("PasswordRequired"))
                .Length(8, 20).WithMessage(_resourceManager.GetString("PasswordLength"))
                .Matches(@"[A-Z]+").WithMessage(_resourceManager.GetString("PasswordUppercase"))
                .Matches(@"[a-z]+").WithMessage(_resourceManager.GetString("PasswordLowercase"))
                .Matches(@"[0-9]+").WithMessage(_resourceManager.GetString("PasswordNumber"))
                .Matches(@"[\W_]+").WithMessage(_resourceManager.GetString("PasswordW"));

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage(_resourceManager.GetString("PasswordsNotMatch"));

            RuleFor(x => x.Email).Must(IsEmailNotExist).WithMessage(_resourceManager.GetString("EmailExist"));
            RuleFor(x => x.Username).Must(IsUsernameNotExist).WithMessage(_resourceManager.GetString("UsernameExist"));
        }

        private bool IsEmailNotExist(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            return user == null ? true : false;
        }

        private bool IsUsernameNotExist(string username)
        {
            var user = _userManager.FindByNameAsync(username).Result;
            return user == null ? true : false;
        }
    }
}