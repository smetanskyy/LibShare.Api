using FluentValidation;
using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Resources;

namespace LibShare.Api.Data.Services
{
    public class EmailValidator : AbstractValidator<EmailApiModel>
    {
        private readonly IRecaptchaService _recaptcha;
        private readonly ResourceManager _resourceManager;

        public EmailValidator(IRecaptchaService recaptcha, ResourceManager resourceManager)
        {
            _recaptcha = recaptcha;
            _resourceManager = resourceManager;

            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.RecaptchaToken)
                .NotNull().WithMessage(_resourceManager.GetString("RecaptchaRequired"))
                .Must(_recaptcha.IsValid).WithMessage(_resourceManager.GetString("RecaptchaInvalid"));

            RuleFor(x => x.Email)
                .NotNull().WithMessage(_resourceManager.GetString("EmailRequired"))
                .EmailAddress().WithMessage(_resourceManager.GetString("EmailInvalid"));
        }
    }

    public class RestoreValidator : AbstractValidator<RestoreApiModel>
    {
        private readonly IRecaptchaService _recaptcha;
        private readonly ResourceManager _resourceManager;

        public RestoreValidator(IRecaptchaService recaptcha, ResourceManager resourceManager)
        {
            _recaptcha = recaptcha;
            _resourceManager = resourceManager;

            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.RecaptchaToken)
                .NotNull().WithMessage(_resourceManager.GetString("RecaptchaRequired"))
                .Must(_recaptcha.IsValid).WithMessage(_resourceManager.GetString("RecaptchaInvalid"));

            RuleFor(x => x.Email)
                .NotNull().WithMessage(_resourceManager.GetString("EmailRequired"))
                .EmailAddress().WithMessage(_resourceManager.GetString("EmailInvalid"));

            RuleFor(x => x.NewPassword).NotNull().WithMessage(_resourceManager.GetString("PasswordRequired"))
                .Length(8, 20).WithMessage(_resourceManager.GetString("PasswordLength"))
                .Matches(@"[A-Z]+").WithMessage(_resourceManager.GetString("PasswordUppercase"))
                .Matches(@"[a-z]+").WithMessage(_resourceManager.GetString("PasswordLowercase"))
                .Matches(@"[0-9]+").WithMessage(_resourceManager.GetString("PasswordNumber"))
                .Matches(@"[\W_]+").WithMessage(_resourceManager.GetString("PasswordW"));

            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage(_resourceManager.GetString("PasswordsNotMatch"));
        }
    }

    public class LoginValidator : AbstractValidator<UserLoginApiModel>
    {
        private readonly IRecaptchaService _recaptcha;
        private readonly ResourceManager _resourceManager;

        public LoginValidator(IRecaptchaService recaptcha, ResourceManager resourceManager)
        {
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
        }
    }

    public class RegisterValidator : AbstractValidator<UserRegisterApiModel>
    {
        private readonly IRecaptchaService _recaptcha;
        private readonly ResourceManager _resourceManager;

        public RegisterValidator(IRecaptchaService recaptcha, ResourceManager resourceManager)
        {
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
        }
    }

    public class ChangePasswordValidator : AbstractValidator<ChangePasswordApiModel>
    {
        private readonly IRecaptchaService _recaptcha;
        private readonly ResourceManager _resourceManager;

        public ChangePasswordValidator(IRecaptchaService recaptcha, ResourceManager resourceManager)
        {
            _recaptcha = recaptcha;
            _resourceManager = resourceManager;

            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.RecaptchaToken)
                .NotNull().WithMessage(_resourceManager.GetString("RecaptchaRequired"))
                .Must(_recaptcha.IsValid).WithMessage(_resourceManager.GetString("RecaptchaInvalid"));

            RuleFor(x => x.OldPassword).NotNull().WithMessage(_resourceManager.GetString("PasswordRequired"))
                .Length(8, 20).WithMessage(_resourceManager.GetString("PasswordLength"))
                .Matches(@"[A-Z]+").WithMessage(_resourceManager.GetString("PasswordUppercase"))
                .Matches(@"[a-z]+").WithMessage(_resourceManager.GetString("PasswordLowercase"))
                .Matches(@"[0-9]+").WithMessage(_resourceManager.GetString("PasswordNumber"))
                .Matches(@"[\W_]+").WithMessage(_resourceManager.GetString("PasswordW"));

            RuleFor(x => x.NewPassword).NotNull().WithMessage(_resourceManager.GetString("PasswordRequired"))
                .Length(8, 20).WithMessage(_resourceManager.GetString("PasswordLength"))
                .Matches(@"[A-Z]+").WithMessage(_resourceManager.GetString("PasswordUppercase"))
                .Matches(@"[a-z]+").WithMessage(_resourceManager.GetString("PasswordLowercase"))
                .Matches(@"[0-9]+").WithMessage(_resourceManager.GetString("PasswordNumber"))
                .Matches(@"[\W_]+").WithMessage(_resourceManager.GetString("PasswordW"));

            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage(_resourceManager.GetString("PasswordsNotMatch"));
        }
    }
}