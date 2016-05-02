﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace PlayerRatings.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Match> Match { get; set; }

        public DbSet<League> League { get; set; }

        public DbSet<LeaguePlayer> LeaguePlayers { get; set; }

        public DbSet<Invite> Invites { get; set; }
    }
}
