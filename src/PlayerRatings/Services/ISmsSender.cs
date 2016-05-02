using System.Threading.Tasks;

namespace PlayerRatings.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
