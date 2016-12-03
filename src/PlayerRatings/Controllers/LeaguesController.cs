using PlayerRatings.Engine.Rating;
using PlayerRatings.Engine.Stats;
using PlayerRatings.Models;
using PlayerRatings.ViewModels.League;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerRatings.Repositories;
using PlayerRatings.Util;

namespace PlayerRatings.Controllers
{
    [Authorize]
    public class LeaguesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILeaguesRepository _leaguesRepository;

        public LeaguesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            ILeaguesRepository leaguesRepository)
        {
            _context = context;
            _userManager = userManager;
            _leaguesRepository = leaguesRepository;
        }

        // GET: Leagues
        public async Task<IActionResult> Index()
        {
            var currentUser = await User.GetApplicationUser(_userManager);

            return
                View(_leaguesRepository.GetLeagues(currentUser).ToList().Select(l => new LeagueViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    CreatedByUserId = l.CreatedByUserId
                }));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var currentUser = await User.GetApplicationUser(_userManager);

            var league = _leaguesRepository.GetUserAuthorizedLeague(currentUser, id);

            if (league == null)
            {
                return NotFound();
            }

            return View(new LeagueDetailsViewModel
            {
                League = league,
                Players = _context.LeaguePlayers.Include(lp => lp.User).Where(lp => lp.LeagueId == league.Id).ToList()
            });
        }

        public IActionResult NoLeagues()
        {
            return View();
        }

        // GET: Leagues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leagues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await User.GetApplicationUser(_userManager);

                var league = new League
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    CreatedByUser = currentUser
                };
                _context.League.Add(league);
                _context.LeaguePlayers.Add(new LeaguePlayer
                {
                    Id = Guid.NewGuid(),
                    League = league,
                    User = currentUser
                });
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Leagues/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await User.GetApplicationUser(_userManager);

            var league = _leaguesRepository.GetAdminAuthorizedLeague(currentUser, id.Value);
            if (league == null)
            {
                return NotFound();
            }
            return View(new LeagueViewModel
            {
                Id = league.Id,
                CreatedByUserId = league.CreatedByUserId,
                Name = league.Name
            });
        }

        // POST: Leagues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeagueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await User.GetApplicationUser(_userManager);

                var league = _leaguesRepository.GetAdminAuthorizedLeague(currentUser, model.Id);
                if (league == null)
                {
                    return NotFound();
                }

                league.Name = model.Name;

                _context.Update(league);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Leagues/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await User.GetApplicationUser(_userManager);

            var league = _leaguesRepository.GetAdminAuthorizedLeague(currentUser, id.Value);
            if (league == null)
            {
                return NotFound();
            }

            return View(new LeagueViewModel
            {
                Id = league.Id,
                Name = league.Name,
                CreatedByUserId = league.CreatedByUserId
            });
        }

        // POST: Leagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var currentUser = await User.GetApplicationUser(_userManager);

            var league = _leaguesRepository.GetAdminAuthorizedLeague(currentUser, id);
            if (league == null)
            {
                return NotFound();
            }

            _context.League.Remove(league);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetBlocked(Guid playerId, bool block)
        {
            var currentUser = await User.GetApplicationUser(_userManager);

            var player = _context.LeaguePlayers.SingleOrDefault(lp => lp.Id == playerId);

            if (player == null)
            {
                return NotFound();
            }

            var league = _leaguesRepository.GetAdminAuthorizedLeague(currentUser, player.LeagueId);
            if (league == null)
            {
                return NotFound();
            }

            player.IsBlocked = block;

            _context.SaveChanges();

            var id = league.Id;
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Leagues/Rating/5
        public async Task<IActionResult> Rating(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await User.GetApplicationUser(_userManager);

            var league = _leaguesRepository.GetUserAuthorizedLeague(currentUser, id.Value);

            if (league == null)
            {
                return NotFound();
            }

            league =
                _context.League.Include(l => l.Matches)
                    .ThenInclude(m => m.FirstPlayer)
                    .Include(l => l.Matches)
                    .ThenInclude(m => m.SecondPlayer)
                    .Single(m => m.Id == id);

            var notBlockedUserIds =
                new HashSet<string>(
                    _context.LeaguePlayers.Where(lp => lp.LeagueId == league.Id && !lp.IsBlocked)
                        .Select(lp => lp.UserId));
            var activeUsers = new HashSet<ApplicationUser>();
            var elo = new EloStat();
            var stats = new List<IStat>
            {
                elo,
                new WinRateStat(),
                new GoalsForAgainstStat(),
                new StreakStat(true),
                new StreakStat(false)
            };
            foreach (var match in league.Matches.OrderBy(m => m.Date))
            {
                if (notBlockedUserIds.Contains(match.FirstPlayerId))
                {
                    activeUsers.Add(match.FirstPlayer);
                }
                if (notBlockedUserIds.Contains(match.SecondPlayerId))
                {
                    activeUsers.Add(match.SecondPlayer);
                }

                foreach (var stat in stats)
                {
                    stat.AddMatch(match);
                }
            }

            var userList = new List<ApplicationUser>(activeUsers);
            var forecast = new Dictionary<string, Dictionary<string, string>>();
            foreach (var appUser in userList)
            {
                var dict = new Dictionary<string, string>();

                foreach (var t in userList)
                {
                    var userRating = elo[appUser];
                    dict[t.Id] = (new Elo(userRating, elo[t], 1, 0).NewRatingAPlayer - userRating).ToString();
                }

                forecast[appUser.Id] = dict;
            }

            var lastMatches = _context.Match.Where(m => m.LeagueId == league.Id).OrderByDescending(m => m.Date).Take(5);

            return
                View(new RatingViewModel(stats, activeUsers.OrderByDescending(u => elo.GetResult(u)), forecast,
                    lastMatches));
        }
    }
}