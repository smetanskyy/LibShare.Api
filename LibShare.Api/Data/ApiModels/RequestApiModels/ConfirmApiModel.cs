namespace LibShare.Api.Data.ApiModels.RequestApiModels
{
    public class ConfirmApiModel
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
        /// Google Recaptcha Token
        /// </summary>
        /// <example>Recaptcha</example>
        public string RecaptchaToken { get; set; }
    }
}
