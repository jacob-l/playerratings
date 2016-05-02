using PlayerRatings.Localization;
using PlayerRatings.Models;
using System.Collections.Generic;

namespace PlayerRatings.Engine.Stats
{
    public class WinRateStat : IStat
    {
        private readonly Dictionary<string, int> _wins = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _total = new Dictionary<string, int>();

        public void AddMatch(Match match)
        {
            _wins[match.FirstPlayer.Id] = _wins.ContainsKey(match.FirstPlayer.Id) ? _wins[match.FirstPlayer.Id] : 0;
            _wins[match.SecondPlayer.Id] = _wins.ContainsKey(match.SecondPlayer.Id) ? _wins[match.SecondPlayer.Id] : 0;
            _total[match.FirstPlayer.Id] = _total.ContainsKey(match.FirstPlayer.Id) ? _total[match.FirstPlayer.Id] : 0;
            _total[match.SecondPlayer.Id] = _total.ContainsKey(match.SecondPlayer.Id) ? _total[match.SecondPlayer.Id] : 0;

            _total[match.FirstPlayer.Id]++;
            _total[match.SecondPlayer.Id]++;

            if (match.FirstPlayerScore == match.SecondPlayerScore)
            {
                return;
            }

            if (match.FirstPlayerScore > match.SecondPlayerScore)
            {
                _wins[match.FirstPlayer.Id]++;
            }
            else
            {
                _wins[match.SecondPlayer.Id]++;
            }
        }

        public string GetResult(ApplicationUser user)
        {
            return ((double) _wins[user.Id]/_total[user.Id]).ToString("N2");
        }

        public string NameLocalizationKey { get; } = nameof(LocalizationKey.WinRate);
    }
}
