using System;
using System.Collections.Generic;

namespace LibShare.Api.Data.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
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

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    class IdDTO
    {
        public long Id { get; set; }
    }
}
