using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LibShare.Api.Data.Entities
{
    public class DbUser : IdentityUser<long>
    {
        public virtual ICollection<DbUserRole> UserRoles { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual Token Token { get; set; }
    }
}
