using System;
using System.ComponentModel.DataAnnotations;

namespace LibShare.Api.Data.ApiModels
{
    public class UserIdVM
    {
        [Required]
        public string Id { get; set; }
    }

    public class UserApiModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
