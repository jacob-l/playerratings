using PlayerRatings.Localization;
using PlayerRatings.Models;
using System.Collections.Generic;

namespace PlayerRatings.Engine.Stats
{
    public class GoalsForAgainstStat : IStat
    {
        private readonly Dictionary<string, int> _for = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _against = new Dictionary<string, int>();

        public void AddMatch(Match match)
        {
            _for[match.FirstPlayer.Id] = _for.ContainsKey(match.FirstPlayer.Id) ? _for[match.FirstPlayer.Id] : 0;
            _for[match.SecondPlayer.Id] = _for.ContainsKey(match.SecondPlayer.Id) ? _for[match.SecondPlayer.Id] : 0;
            _against[match.FirstPlayer.Id] = _against.ContainsKey(match.FirstPlayer.Id) ? _against[match.FirstPlayer.Id] : 0;
            _against[match.SecondPlayer.Id] = _against.ContainsKey(match.SecondPlayer.Id) ? _against[match.SecondPlayer.Id] : 0;

            _for[match.FirstPlayer.Id] += match.FirstPlayerScore;
            _against[match.FirstPlayer.Id] += match.SecondPlayerScore;
            _for[match.SecondPlayer.Id] += match.SecondPlayerScore;
            _against[match.SecondPlayer.Id] += match.FirstPlayerScore;
        }

        public string GetResult(ApplicationUser user)
        {
            if (_against[user.Id] == 0)
            {
                return "∞";
            }

            return ((double)_for[user.Id] / _against[user.Id]).ToString("N2");
        }

        public string NameLocalizationKey { get; } = nameof(LocalizationKey.AgainstFor);
    }
}
