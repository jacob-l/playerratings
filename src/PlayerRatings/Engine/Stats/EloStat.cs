using PlayerRatings.Engine.Rating;
using PlayerRatings.Models;
using System.Collections.Generic;
using PlayerRatings.Localization;

namespace PlayerRatings.Engine.Stats
{
    public class EloStat : IStat
    {
        private readonly Dictionary<string, int> _dict = new Dictionary<string, int>();

        public void AddMatch(Match match)
        {
            var firstUserScore = 1.0;
            if (match.SecondPlayerScore > match.FirstPlayerScore)
            {
                firstUserScore = 0;
            }
            else if (match.SecondPlayerScore == match.FirstPlayerScore)
            {
                firstUserScore = 0.5;
            }

            var rating = new Elo(_dict.ContainsKey(match.FirstPlayer.Id) ? _dict[match.FirstPlayer.Id] : Elo.DefaultRating,
                _dict.ContainsKey(match.SecondPlayer.Id) ? _dict[match.SecondPlayer.Id] : Elo.DefaultRating, firstUserScore,
                1 - firstUserScore, Elo.K * match.Factor.GetValueOrDefault(1));

            _dict[match.FirstPlayer.Id] = rating.NewRatingAPlayer;
            _dict[match.SecondPlayer.Id] = rating.NewRatingBPlayer;
        }

        public string GetResult(ApplicationUser user)
        {
            return _dict.ContainsKey(user.Id) ? _dict[user.Id].ToString() : "";
        }

        public string NameLocalizationKey => nameof(LocalizationKey.Elo);

        public int this[ApplicationUser user] => _dict.ContainsKey(user.Id) ? _dict[user.Id] : 0;
    }
}
