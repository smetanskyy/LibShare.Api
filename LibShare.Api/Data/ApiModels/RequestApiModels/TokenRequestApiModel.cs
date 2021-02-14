namespace LibShare.Api.Data.ApiModels.RequestApiModels
{
    public class TokenRequestApiModel
    {
        /// <summary>
        /// A token containing user ID, email, and roles.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Disposable refresh token.
        /// </summary>     
        public string RefreshToken { get; set; }
    }
}
