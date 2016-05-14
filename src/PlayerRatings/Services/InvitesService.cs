﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Localization;
using PlayerRatings.Controllers;
using PlayerRatings.Localization;
using PlayerRatings.Models;

namespace PlayerRatings.Services
{
    public class InvitesService : IInvitesService
    {
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUrlHelper _urlHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<InvitesService> _localizer;

        public InvitesService(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            IEmailSender emailSender, IUrlHelper urlHelper, IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<InvitesService> localizer)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _urlHelper = urlHelper;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
        }

        public string GetInviteUrl(Guid inviteId)
        {
            return _urlHelper.Action(nameof(AccountController.Register), "Account", new {inviteId},
                _httpContextAccessor.HttpContext.Request.Scheme);
        }

        public async Task SendEmail(Invite invite)
        {
            var callbackUrl = GetInviteUrl(invite.Id);
            var title = _localizer[nameof(LocalizationKey.InvitedYou), invite.InvitedBy.DisplayName];
            var msg = _localizer[nameof(LocalizationKey.ConfirmAccount), callbackUrl];
            await _emailSender.SendEmailAsync(invite.CreatedUser.Email, title, msg);
        }

        public async Task<ApplicationUser> Invite(string email, ApplicationUser invitedBy, League league)
        {
            var invited = await _userManager.FindByEmailAsync(email);

            Invite invitation = null;
            if (invited == null)
            {
                var user = new ApplicationUser
                {
                    DisplayName = email,
                    UserName = email,
                    Email = email
                };
                var result = await _userManager.CreateAsync(user, Guid.NewGuid() + "Aa1!");

                if (!result.Succeeded)
                {
                    throw new Exception(_localizer[nameof(LocalizationKey.ErrorOccurred)]);
                }

                invitation = new Invite
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTimeOffset.Now,
                    CreatedUser = user,
                    InvitedBy = invitedBy
                };

                _context.Invites.Add(invitation);
                invited = user;
            }

            if (league != null && !_context.LeaguePlayers.Any(lp => lp.LeagueId == league.Id && lp.UserId == invited.Id))
            {
                _context.LeaguePlayers.Add(new LeaguePlayer
                {
                    Id = Guid.NewGuid(),
                    League = league,
                    User = invited
                });
            }

            _context.SaveChanges();

            if (invitation != null)
            {
                SendEmail(invitation);
            }

            return invited;
        }
    }
}