using System.Threading.Tasks;

namespace PlayerRatings.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
