using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using PlayerRatings.Models;
using PlayerRatings.Services;
using PlayerRatings.Util;
using PlayerRatings.ViewModels.Invites;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerRatings.Controllers
{
    [Authorize]
    public class InvitesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInvitesService _invitesService;

        public InvitesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IInvitesService invitesService)
        {
            _context = context;
            _userManager = userManager;
            _invitesService = invitesService;
        }

        private League GetAuthorizedLeague(Guid id, ApplicationUser user)
        {
            var league = _context.League.Single(m => m.Id == id);
            if (league == null)
            {
                return null;
            }

            if (!_context.LeaguePlayers.Any(lp => lp.LeagueId == id && lp.UserId == user.Id))
            {
                return null;
            }

            return league;
        }

        // GET: Invites
        public async Task<IActionResult> Index()
        {
            var currentUser = await User.GetApplicationUser(_userManager);

            return View(await _context.Invites.Include(i => i.CreatedUser).Where(i => i.InvitedById == currentUser.Id).ToListAsync());
        }

        // GET: Invites/Create
        public async Task<IActionResult> Create(Guid? leagueId)
        {
            if (leagueId == null)
            {
                return View();
            }
            var currentUser = await User.GetApplicationUser(_userManager);

            var league = GetAuthorizedLeague(leagueId.Value, currentUser);

            if (league == null)
            {
                return HttpNotFound();
            }

            return View(new InviteViewModel
            {
                LeagueId = leagueId
            });
        }

        // POST: Invites/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InviteViewModel invite)
        {
            var currentUser = await User.GetApplicationUser(_userManager);

            if (ModelState.IsValid)
            {
                League league = null;
                if (invite.LeagueId.HasValue)
                {
                    league = GetAuthorizedLeague(invite.LeagueId.Value, currentUser);

                    if (league == null)
                    {
                        return HttpNotFound();
                    }
                }

                try
                {
                    await _invitesService.Invite(invite.Email, currentUser, league);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(invite);
                }

                return RedirectToAction("Index");
            }

            return View(invite);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var currentUser = await User.GetApplicationUser(_userManager);

            var invite = await _context.Invites.Include(i => i.CreatedUser).SingleAsync(m => m.Id == id);
            if (invite == null || invite.InvitedById != currentUser.Id)
            {
                return HttpNotFound();
            }

            return View(new InviteViewModel
            {
                Email = invite.CreatedUser.Email
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InviteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await User.GetApplicationUser(_userManager);

                var invite = await _context.Invites.Include(i => i.CreatedUser).SingleAsync(m => m.Id == model.Id);
                if (invite == null || invite.InvitedById != currentUser.Id)
                {
                    return HttpNotFound();
                }

                var createdUser = invite.CreatedUser;

                createdUser.Email = model.Email;
                createdUser.DisplayName = model.Email;
                createdUser.UserName = model.Email;

                await _userManager.UpdateAsync(createdUser);

                await _invitesService.SendEmail(invite);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Invites/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var invite = await _context.Invites.Include(i => i.CreatedUser).SingleOrDefaultAsync(m => m.Id == id);
            if (invite == null)
            {
                return HttpNotFound();
            }

            return View(invite);
        }

        // POST: Invites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var invite = await _context.Invites.SingleAsync(m => m.Id == id);
            _context.Invites.Remove(invite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
