using LibShare.Api.Data.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace LibShare.Api.Data.Interfaces
{
    public interface IJwtService
    {
        IEnumerable<Claim> SetClaims(DbUser user);
        string CreateToken(IEnumerable<Claim> claims);
        string CreateRefreshToken();
        IEnumerable<Claim> GetClaimsFromExpiredToken(string token);
    }
}
