﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlayerRatings.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Match> Match { get; set; }

        public DbSet<League> League { get; set; }

        public DbSet<LeaguePlayer> LeaguePlayers { get; set; }

        public DbSet<Invite> Invites { get; set; }
    }
}
