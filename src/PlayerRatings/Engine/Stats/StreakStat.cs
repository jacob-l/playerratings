using PlayerRatings.Localization;
using PlayerRatings.Models;
using System.Collections.Generic;

namespace PlayerRatings.Engine.Stats
{
    public class StreakStat : IStat
    {
        private readonly bool _wins;

        private readonly Dictionary<string, int> _streak = new Dictionary<string, int>();

        public StreakStat(bool wins)
        {
            _wins = wins;
        }

        public void AddMatch(Match match)
        {
            _streak[match.FirstPlayer.Id] = _streak.ContainsKey(match.FirstPlayer.Id) ? _streak[match.FirstPlayer.Id] : 0;
            _streak[match.SecondPlayer.Id] = _streak.ContainsKey(match.SecondPlayer.Id) ? _streak[match.SecondPlayer.Id] : 0;

            if (match.FirstPlayerScore == match.SecondPlayerScore)
            {
                _streak[match.FirstPlayer.Id] = 0;
                _streak[match.SecondPlayer.Id] = 0;

                return;
            }

            if ((match.FirstPlayerScore > match.SecondPlayerScore && _wins) ||
                (match.SecondPlayerScore > match.FirstPlayerScore && !_wins))
            {
                _streak[match.FirstPlayer.Id]++;
                _streak[match.SecondPlayer.Id] = 0;
            }
            else
            {
                _streak[match.FirstPlayer.Id] = 0;
                _streak[match.SecondPlayer.Id]++;
            }
        }

        public string GetResult(ApplicationUser user)
        {
            return _streak[user.Id].ToString();
        }

        public string NameLocalizationKey => _wins ? nameof(LocalizationKey.WinStreak) : nameof(LocalizationKey.LooseStreak);
    }
}
