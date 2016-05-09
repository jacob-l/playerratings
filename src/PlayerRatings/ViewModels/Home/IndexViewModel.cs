
namespace PlayerRatings.ViewModels.Home
{
    public class IndexViewModel
    {
        public IndexViewModel(int leagues, int players, int matches)
        {
            Leagues = leagues;
            Players = players;
            Matches = matches;
        }

        public int Leagues { get; }

        public int Players { get; }

        public int Matches { get; }
    }
}
