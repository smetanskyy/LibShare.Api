using FluentValidation;
using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibShare.Api.Data.Services
{

    public class LoginValidator : AbstractValidator<UserLoginApiModel>
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly IRecaptchaService _recaptcha;

        private DbUser _user;

        public LoginValidator(UserManager<DbUser> userManager, IRecaptchaService recaptcha)
        {
            _userManager = userManager;
            _recaptcha = recaptcha;

            CascadeMode = CascadeMode.Stop;

            //RuleFor(x => x.RecaptchaToken)
            //    .NotNull().WithMessage("Recaptcha є обов'язковою!")
            //    .Must(_recaptcha.IsValid).WithMessage("Роботи атакують!");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Електронна пошта є обов'язковою!")
                .EmailAddress().WithMessage("Введіть дійсну електронну пошту!");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Пароль є обов'язковим!")
                .Length(8, 20).WithMessage("Пароль має містити мінімум 8 символів і максимум 20 (включно)!")
                .Matches(@"[A-Z]+").WithMessage("Пароль має містити щонайменше одну літеру верхнього регістру!")
                .Matches(@"[a-z]+").WithMessage("Пароль має містити щонайменше одну літеру нижнього регістру!")
                .Matches(@"[0-9]+").WithMessage("Пароль має містити щонайменше одну цифру!")
                .Matches(@"[\W_]+").WithMessage("Пароль має містити щонайменше один спеціальний символ!");

            RuleFor(x => x.Email).Must(IsEmailExist).WithMessage("Логін або пароль неправильний!");
            RuleFor(x => x.Password).Must(IsPasswordCorrect).WithMessage("Логін або пароль неправильний!");
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

        public RegisterValidator(UserManager<DbUser> userManager, IRecaptchaService recaptcha)
        {
            _userManager = userManager;
            _recaptcha = recaptcha;

            CascadeMode = CascadeMode.Stop;

            //RuleFor(x => x.RecaptchaToken)
            //    .NotNull().WithMessage("Recaptcha є обов'язковою!")
            //    .Must(_recaptcha.IsValid).WithMessage("Роботи атакують!");

            RuleFor(x => x.Email).NotNull().WithMessage("Електронна пошта є обов'язковою!")
                .EmailAddress().WithMessage("Введіть дійсну електронну пошту!");

            RuleFor(x => x.Username).NotNull().WithMessage("Ім'я користувача є обов'язковим!")
                .Length(2, 20).WithMessage("Ім'я користувача має містити мінімум 2 символа і максимум 20 (включно)!")
                .Matches(@"[a-zA-z]+").WithMessage("Пароль має містити щонайменше одну латинську літеру!");

            RuleFor(x => x.Password).NotNull().WithMessage("Пароль є обов'язковим!")
                .Length(8, 20).WithMessage("Пароль має містити мінімум 8 символів і максимум 20 (включно)!")
                .Matches(@"[A-Z]+").WithMessage("Пароль має містити щонайменше одну латинську літеру верхнього регістру!")
                .Matches(@"[a-z]+").WithMessage("Пароль має містити щонайменше одну латинську літеру нижнього регістру!")
                .Matches(@"[0-9]+").WithMessage("Пароль має містити щонайменше одну цифру!")
                .Matches(@"[\W_]+").WithMessage("Пароль має містити щонайменше один спеціальний символ!");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Паролі не співпадають!");

            RuleFor(x => x.Email).Must(IsEmailNotExist).WithMessage("Електронна пошта вже існує!");
            RuleFor(x => x.Username).Must(IsUsernameNotExist).WithMessage("Ім'я користувача вже існує!");
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