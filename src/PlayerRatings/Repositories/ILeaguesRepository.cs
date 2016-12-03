using System;
using System.Collections.Generic;
using PlayerRatings.Models;

namespace PlayerRatings.Repositories
{
    public interface ILeaguesRepository
    {
        IEnumerable<League> GetLeagues(ApplicationUser user);

        League GetUserAuthorizedLeague(ApplicationUser user, Guid leagueId);

        League GetAdminAuthorizedLeague(ApplicationUser user, Guid leagueId);
    }
}
