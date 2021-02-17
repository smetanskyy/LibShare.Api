using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces.IRepositories
{
    public interface IAccessProhibitedRepository<Type, TypeId> where Type : class
    {
        Task<Type> GetById(TypeId id);
    }
}
