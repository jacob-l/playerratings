using PlayerRatings.Models;

namespace PlayerRatings.Engine.Stats
{
    public interface IStat
    {
        void AddMatch(Match match);

        string GetResult(ApplicationUser user);

        string NameLocalizationKey { get; }
    }
}
