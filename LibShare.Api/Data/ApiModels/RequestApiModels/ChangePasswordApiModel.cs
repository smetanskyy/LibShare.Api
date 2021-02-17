using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Api.Data.ApiModels.RequestApiModels
{
    public class ChangePasswordApiModel
    {
        /// <summary>
        /// User old password
        /// </summary>
        /// <example>Qwerty1-</example>
        public string OldPassword { get; set; }

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
