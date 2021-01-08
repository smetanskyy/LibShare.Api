using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibShare.Api.Data.Entities
{
    [Table("tblToken")]
    public class Token
    {
        [Key, ForeignKey("User")]
        public long Id { get; set; }
        public virtual DbUser User { get; set; }

        /// <summary>
        /// Get or set refresh token
        /// </summary>
        [Required, StringLength(100)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Get or set refresh token expiry time
        /// </summary>
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
