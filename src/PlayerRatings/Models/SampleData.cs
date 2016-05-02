using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace PlayerRatings.Models
{
    public class SampleData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            if (!EnableTestData)
            {
                return;
            }

            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            context.Database.Migrate();

            if (context.League.Any())
            {
                return;
            }

            var firstUser = new ApplicationUser { UserName = "FirstUser", Email = "fu@test.test" };
            await userManager.CreateAsync(firstUser);

            var secondUser = new ApplicationUser {UserName = "SecondUser", Email = "su@test.test" };
            await userManager.CreateAsync(secondUser);

            var league = new League { Id = Guid.NewGuid(), Name = "Hwdtech single", CreatedByUser = firstUser };
            context.League.Add(league);

            context.SaveChanges();

            context.Match.AddRange(
                new Match
                {
                    Id = Guid.NewGuid(),
                    League = league,
                    FirstPlayer = firstUser,
                    SecondPlayer = secondUser,
                    FirstPlayerScore = 10,
                    SecondPlayerScore = 5
                },
                new Match
                {
                    Id = Guid.NewGuid(),
                    League = league,
                    FirstPlayer = firstUser,
                    SecondPlayer = secondUser,
                    FirstPlayerScore = 10,
                    SecondPlayerScore = 3
                },
                new Match
                {
                    Id = Guid.NewGuid(),
                    League = league,
                    FirstPlayer = firstUser,
                    SecondPlayer = secondUser,
                    FirstPlayerScore = 9,
                    SecondPlayerScore = 10
                },
                new Match
                {
                    Id = Guid.NewGuid(),
                    League = league,
                    FirstPlayer = firstUser,
                    SecondPlayer = secondUser,
                    FirstPlayerScore = 1,
                    SecondPlayerScore = 10
                },
                new Match
                {
                    Id = Guid.NewGuid(),
                    League = league,
                    FirstPlayer = firstUser,
                    SecondPlayer = secondUser,
                    FirstPlayerScore = 10,
                    SecondPlayerScore = 7
                },
                new Match
                {
                    Id = Guid.NewGuid(),
                    League = league,
                    FirstPlayer = firstUser,
                    SecondPlayer = secondUser,
                    FirstPlayerScore = 0,
                    SecondPlayerScore = 10
                }
                );

            context.SaveChanges();
        }

        public static bool EnableTestData { get; } = false;
    }
}
