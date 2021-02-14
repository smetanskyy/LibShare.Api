namespace LibShare.Api.Data.ApiModels.ResponseApiModels
{
    /// <summary>
    /// A class for return a error in response.
    /// </summary>
    public class MessageApiModel
    {
        /// <summary>
        /// Initializes a new instance of response result.
        /// </summary>
        /// <param name="message">Sets the message</param>
        public MessageApiModel(string message = null)
        {
            Message = message;
        }

        /// <summary>
        /// Gets or sets the message for response description.
        /// </summary>
        public string Message { get; set; }
    }
}