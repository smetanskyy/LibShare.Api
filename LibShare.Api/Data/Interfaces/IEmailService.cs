using System.Threading.Tasks;

namespace LibShare.Api.Data.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendAsync(string email, string subject, string content);
    }
}
