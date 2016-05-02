using System.Collections.Generic;
using PlayerRatings.Engine.Stats;
using PlayerRatings.Models;

namespace PlayerRatings.ViewModels.League
{
    public class RatingViewModel
    {
        public RatingViewModel(IEnumerable<IStat> stats, IEnumerable<ApplicationUser> users,
            Dictionary<string, Dictionary<string, string>> forecast, IEnumerable<Models.Match> lastMatches)
        {
            Stats = stats;
            Users = users;
            Forecast = forecast;
            LastMatches = lastMatches;
        }

        public IEnumerable<IStat> Stats { get; private set; }

        public IEnumerable<ApplicationUser> Users { get; private set; }

        public Dictionary<string, Dictionary<string, string>> Forecast { get; private set; }

        public IEnumerable<Models.Match> LastMatches { get; private set; }
    }
}
