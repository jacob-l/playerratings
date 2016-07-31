using System;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using PlayerRatings.Controllers;
using Xunit;
using FluentAssertions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http.Features.Authentication;
using Microsoft.AspNet.Http.Features.Authentication.Internal;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using PlayerRatings.Models;
using PlayerRatings.Repositories;
using PlayerRatings.Services;

namespace PlayerRatings.UnitTests.Controllers
{
    //About Mocking - http://www.jerriepelser.com/blog/unit-testing-controllers-aspnet5

    public class MatchesControllerTests
    {
        private ApplicationDbContext Context { get; }
        private UserManager<ApplicationUser> UserManager { get; }

        private IServiceProvider ServiceProvider { get; }

        private Mock<IStringLocalizer<MatchesController>> StringLocalizerMock { get; } = new Mock<IStringLocalizer<MatchesController>>();
        private Mock<IInvitesService> InviteServiceMock { get; } = new Mock<IInvitesService>();
        private Mock<ILeaguesRepository> LeaguesRepositoryMock { get; } = new Mock<ILeaguesRepository>();

        public MatchesControllerTests()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var context = new DefaultHttpContext();
            context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature());
            services.AddSingleton<IHttpContextAccessor>(h => new HttpContextAccessor { HttpContext = context });

            ServiceProvider = services.BuildServiceProvider();

            Context = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            UserManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async void CreatePageForEmptyAccountTest()
        {
            // Arrange
            const string userId = "MyUserId";
            Context.Add(new ApplicationUser
            {
                Id = userId
            });
            Context.SaveChanges();
            var validPrincipal = new ClaimsPrincipal(
                new[]
                {
                    new ClaimsIdentity(
                        new[] {new Claim(ClaimTypes.NameIdentifier, userId) })
                });

            var httpContext = new Mock<HttpContext>();
            httpContext.SetupProperty(o => o.User, validPrincipal);

            var controller = new MatchesController(Context, UserManager, StringLocalizerMock.Object,
                InviteServiceMock.Object, LeaguesRepositoryMock.Object)
            {
                ActionContext = new ActionContext
                {
                    HttpContext = httpContext.Object
                }
            };

            // Act
            var result = await controller.Create(null);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>().Which.ControllerName.Should().Be("Leagues");
            result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("NoLeagues");
        }
    }
}
