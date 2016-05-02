using System.Collections.Generic;
using PlayerRatings.Models;

namespace PlayerRatings.ViewModels.League
{
    public class LeagueDetailsViewModel
    {
        public Models.League League { get; set; }

        public IEnumerable<LeaguePlayer> Players { get; set; }
    }
}
