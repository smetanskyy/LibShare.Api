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
        public string Photo { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    class IdDTO
    {
        public string Id { get; set; }
    }
}
