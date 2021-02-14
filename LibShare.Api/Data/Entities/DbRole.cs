using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LibShare.Api.Data.Entities
{
    public class DbRole : IdentityRole
    {
        public virtual ICollection<DbUserRole> UserRoles { get; set; }
    }
}
