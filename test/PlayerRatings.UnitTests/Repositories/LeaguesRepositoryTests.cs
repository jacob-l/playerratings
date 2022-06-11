using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using PlayerRatings.Models;
using PlayerRatings.Repositories;
using System;
using System.Linq;
using Xunit;

namespace PlayerRatings.UnitTests.Repositories
{
    public class LeaguesRepositoryTests
    {
        private ApplicationDbContext Context { get; }

        private ApplicationUser User1 { get; }
        private ApplicationUser User2 { get; }

        private League League1 { get; }
        private League League2 { get; }

        private LeaguePlayer LeaguePlayer1 { get; }
        private LeaguePlayer LeaguePlayer2 { get; }

        public LeaguesRepositoryTests()
        {
            var services = new ServiceCollection();
            services
                .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("in-memory"));

            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var serviceProvider = services.BuildServiceProvider();

            Context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            User1 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString()
            };
            User2 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString()
            };

            League1 = new League
            {
                CreatedByUser = User1,
                Id = Guid.NewGuid()
            };
            League2 = new League
            {
                CreatedByUser = User2,
                Id = Guid.NewGuid()
            };

            LeaguePlayer1 = new LeaguePlayer
            {
                Id = Guid.NewGuid(),
                User = User1,
                League = League1
            };
            LeaguePlayer2 = new LeaguePlayer
            {
                Id = Guid.NewGuid(),
                User = User2,
                League = League2
            };
        }

        [Fact]
        public void GetLeaguesEmptyTest()
        {
            // Arrange
            var leaguesRepository = new LeaguesRepository(Context);

            // Act
            var result = leaguesRepository.GetLeagues(User1);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetLeaguesTest()
        {
            // Arrange
            Context.Users.Add(User1);
            Context.League.Add(League1);
            Context.LeaguePlayers.Add(LeaguePlayer1);
            Context.Users.Add(User2);
            Context.League.Add(League2);
            Context.LeaguePlayers.Add(LeaguePlayer2);
            var league3 = new League
            {
                Id = Guid.NewGuid(),
                CreatedByUser = User2
            };
            Context.League.Add(league3);
            Context.LeaguePlayers.Add(new LeaguePlayer
            {
                Id = Guid.NewGuid(),
                User = User1,
                League = league3
            });
            Context.SaveChanges();

            var leaguesRepository = new LeaguesRepository(Context);

            // Act
            var result = leaguesRepository.GetLeagues(User1).ToList();

            // Assert
            result.Should().Contain(League1);
            result.Should().Contain(league3);
            result.Should().HaveCount(2);
        }

        [Fact]
        public void GetAdminAuthorizedLeagueTest()
        {
            // Arrange
            Context.Users.Add(User1);
            Context.League.Add(League1);
            Context.Users.Add(User2);
            Context.League.Add(League2);
            Context.SaveChanges();
            var leaguesRepository = new LeaguesRepository(Context);

            // Act
            var authorizedResult = leaguesRepository.GetAdminAuthorizedLeague(User1, League1.Id);
            var notAuthorizedResult = leaguesRepository.GetAdminAuthorizedLeague(User1, League2.Id);

            // Assert
            authorizedResult.Should().NotBeNull();
            authorizedResult.Should().BeSameAs(League1);
            notAuthorizedResult.Should().BeNull();
        }

        [Fact]
        public void GetUserAuthorizedLeagueTest()
        {
            // Arrange
            Context.Users.Add(User1);
            Context.League.Add(League1);
            Context.LeaguePlayers.Add(LeaguePlayer1);
            Context.Users.Add(User2);
            Context.League.Add(League2);
            Context.LeaguePlayers.Add(LeaguePlayer2);
            Context.SaveChanges();

            var leaguesRepository = new LeaguesRepository(Context);

            // Act
            var authorizedResult = leaguesRepository.GetUserAuthorizedLeague(User1, League1.Id);
            var notAuthorizedResult = leaguesRepository.GetUserAuthorizedLeague(User2, League1.Id);

            // Assert
            authorizedResult.Should().NotBeNull();
            authorizedResult.Should().BeSameAs(League1);
            notAuthorizedResult.Should().BeNull();
        }
    }
}
