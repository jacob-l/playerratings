using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlayerRatings.Models;

namespace PlayerRatings.Repositories
{
    public class LeaguesRepository : ILeaguesRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaguesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<League> GetLeagues(ApplicationUser user)
        {
            return _context.LeaguePlayers.Include(lp => lp.League).Where(lp => lp.UserId == user.Id)
                .Select(lp => lp.League)
                .Distinct()
                .ToList();
        }

        public League GetUserAuthorizedLeague(ApplicationUser user, Guid leagueId)
        {
            var league = _context.League.Single(m => m.Id == leagueId);
            if (league == null)
            {
                return null;
            }

            return _context.LeaguePlayers.Any(lp => lp.LeagueId == leagueId && lp.UserId == user.Id) ? league : null;
        }

        public League GetAdminAuthorizedLeague(ApplicationUser user, Guid leagueId)
        {
            return _context.League.SingleOrDefault(m => m.Id == leagueId && m.CreatedByUserId == user.Id);
        }
    }
}
