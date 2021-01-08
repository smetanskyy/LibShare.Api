using Newtonsoft.Json;
using System.Collections.Generic;

namespace LibShare.Api.Data.DTO
{
    public class RecaptchaResponseDTO
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
