using System.ComponentModel.DataAnnotations;

namespace LibShare.Api.Data.ApiModels.RequestApiModels
{
    public class UserLoginApiModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RecaptchaToken { get; set; }
    }
}
