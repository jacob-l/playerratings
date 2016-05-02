using System;
using System.Threading.Tasks;
using PlayerRatings.Models;

namespace PlayerRatings.Services
{
    public interface IInvitesService
    {
        string GetInviteUrl(Guid inviteId);
        Task SendEmail(Invite invite);
        Task<ApplicationUser> Invite(string email, ApplicationUser invitedBy, League league);
    }
}
