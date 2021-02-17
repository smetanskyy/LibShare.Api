namespace LibShare.Api.Data.ApiModels.RequestApiModels
{
    public class RestoreApiModel
    {
        /// <summary>
        /// User email
        /// </summary>
        /// <example>example@gmail.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Secure token for reset password
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// User new password
        /// </summary>
        /// <example>Qwerty1-</example>
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm new password
        /// </summary>
        /// <example>Qwerty1-</example>
        public string ConfirmNewPassword { get; set; }

        /// <summary>
        /// Google Recaptcha Token
        /// </summary>
        /// <example>Recaptcha</example>
        public string RecaptchaToken { get; set; }
    }
}
