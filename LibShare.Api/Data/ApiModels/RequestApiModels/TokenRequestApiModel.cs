using System.ComponentModel.DataAnnotations;

namespace LibShare.Api.Data.ApiModels.RequestApiModels
{
    public class TokenRequestApiModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
