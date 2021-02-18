namespace LibShare.Api.Data.ApiModels.RequestApiModels
{
    public class UserRegisterApiModel
    {
        /// <summary>
        /// User email
        /// </summary>     
        /// <example>example@gmail.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Login or email
        /// </summary>
        /// <example>User</example>
        public string UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        /// <example>Qwerty1-</example>
        public string Password { get; set; }

        /// <summary>
        /// User confirm password
        /// </summary>
        /// <example>Qwerty1-</example>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Google Recaptcha Token
        /// </summary>
        /// <example>Recaptcha</example>
        public string RecaptchaToken { get; set; }
    }
}
