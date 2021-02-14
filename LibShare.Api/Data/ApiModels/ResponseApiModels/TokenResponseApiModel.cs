namespace LibShare.Api.Data.ApiModels.ResponseApiModels
{
    /// <summary>
    /// A class for return authenticate results in response.
    /// </summary>
    public class TokenResponseApiModel
    {
        /// <summary>
        /// Initializes a new instance of authenticate result model.
        /// </summary>
        public TokenResponseApiModel() { }

        /// <summary>
        /// Initializes a new instance of authenticate result model.
        /// </summary>
        /// <param name="token">Sets the token.</param>
        /// <param name="refreshToken">Sets the refresh token.</param>
        public TokenResponseApiModel(string token = null, string refreshToken = null)
        {
            Token = token;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// Gets or sets the token for response result.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Gets or sets the refresh token for response result.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
