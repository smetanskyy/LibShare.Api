using FluentValidation;
using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

            RuleFor(x => x.UserName).NotNull().WithMessage(_resourceManager.GetString("UsernameRequired"))
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

    public class ImageBase64Validator : AbstractValidator<ImageApiModel>
    {
        private readonly ResourceManager _resourceManager;
        public ImageBase64Validator(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage(_resourceManager.GetString("PhotoBase64Required"))
                .Must(e => e.Contains("image")).WithMessage(_resourceManager.GetString("PhotoBase64FormatType"))
                .Must(IsBase64).WithMessage(_resourceManager.GetString("PhotoBase64Format"));
        }

        private bool IsBase64(string imagebase64)
        {
            string base64 = imagebase64;
            if (base64.Contains(","))
            {
                base64 = base64.Split(',')[1];
            }
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out _);
        }
    }

    public class UserApiModelValidator : AbstractValidator<UserApiModel>
    {
        private readonly ResourceManager _resourceManager;
        public UserApiModelValidator(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email).NotNull().WithMessage(_resourceManager.GetString("EmailRequired"))
                .EmailAddress().WithMessage(_resourceManager.GetString("EmailInvalid"));

            RuleFor(x => x.UserName).NotNull().WithMessage(_resourceManager.GetString("UsernameRequired"))
                .Length(2, 20).WithMessage(_resourceManager.GetString("UsernameLength"));

            RuleFor(x => x.DateOfBirth)
                .Must(IsValidDate).WithMessage(_resourceManager.GetString("DateOfBirthFormat"));
        }

        private bool IsValidDate(DateTime? date)
        {
            if (date == null)
                return true;

            return !date.Equals(default(DateTime));
        }
    }
}