using System.Threading.Tasks;

namespace SimpleWorkTimeTracker.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
