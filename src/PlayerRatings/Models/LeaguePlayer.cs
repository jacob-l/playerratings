using System;

namespace PlayerRatings.Models
{
    public class LeaguePlayer
    {
        public Guid Id { get; set; }

        public bool IsBlocked { get; set; }

        public string UserId { get; set; }

        public Guid LeagueId { get; set; }

        public virtual ApplicationUser User { get; set; }
        
        public virtual League League { get; set; }
    }
}
