using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces.IRepositories;
using System;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Repositories
{
    public class AccessProhibitedRepository : IAccessProhibitedRepository<AccessProhibited, string>
    {
        public Task<AccessProhibited> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
