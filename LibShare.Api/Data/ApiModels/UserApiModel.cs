using System;

namespace LibShare.Api.Data.ApiModels
{
    public class UserApiModel
    {
        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Login or email
        /// </summary>
        /// <example>User</example>
        public string UserName { get; set; }

        /// <summary>
        /// User email
        /// </summary>     
        /// <example>example@gmail.com</example>
        public string Email { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        /// <example>Іван</example>
        public string Name { get; set; }

        /// <summary>
        /// User surname
        /// </summary>
        /// <example>Іванов</example>
        public string Surname { get; set; }

        /// <summary>
        /// User photo
        /// </summary>
        /// <example>PhotoName.jpg</example>
        public string Photo { get; set; }

        /// <summary>
        /// User phone
        /// </summary>
        /// <example>+380670000000</example>
        public string Phone { get; set; }

        /// <summary>
        /// User date of birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
    }
}
