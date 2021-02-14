using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibShare.Api.Data.ViewModels
{
    public class UserIdVM
    {
        [Required]
        public string Id { get; set; }
    }

    public class UserVM
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
