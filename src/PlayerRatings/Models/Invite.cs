using System;

namespace PlayerRatings.Models
{
    public class Invite
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string InvitedById { get; set; }

        public virtual ApplicationUser InvitedBy { get; set; }

        public virtual ApplicationUser CreatedUser { get; set; }
    }
}
