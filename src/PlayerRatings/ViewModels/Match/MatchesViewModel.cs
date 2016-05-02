using System;
using System.Collections.Generic;

namespace PlayerRatings.ViewModels.Match
{
    public class MatchesViewModel
    {
        public MatchesViewModel(IEnumerable<Models.Match> matches, Guid leagueId, int pagesCount, int currentPage)
        {
            Matches = matches;
            LeagueId = leagueId;
            PagesCount = pagesCount;
            CurrentPage = currentPage;
        }

        public IEnumerable<Models.Match> Matches { get; private set; }

        public int PagesCount { get; private set; }

        public int CurrentPage { get; private set; }

        public Guid LeagueId { get; private set; }
    }
}
