using System;
using System.ComponentModel.DataAnnotations;

namespace PlayerRatings.ViewModels.Invites
{
    public class InviteViewModel
    {
        public Guid? LeagueId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Guid Id { get; set; }
    }
}
