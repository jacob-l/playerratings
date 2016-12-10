using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerRatings.Controllers;
using PlayerRatings.Models;
using PlayerRatings.ViewModels.Account;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PlayerRatings.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        private ApplicationDbContext Context { get; }

        private UserManager<ApplicationUser> UserManager { get; }

        private SignInManager<ApplicationUser> SignInManager { get; }

        private Mock<ILoggerFactory> LoggerMock { get; } = new Mock<ILoggerFactory>();

        private IServiceProvider ServiceProvider { get; }

        private Mock<Services.IEmailSender> EmailSenderMock { get; } = new Mock<Services.IEmailSender>();

        private Mock<IStringLocalizer<AccountController>> StringLocalizerMock { get; } = new Mock<IStringLocalizer<AccountController>>();

        private Mock<SignInManager<ApplicationUser>> SignInManagerMock { get; }

        public AccountControllerTests()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework()
                .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var context = new DefaultHttpContext();
            context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature());
            services.AddSingleton<IHttpContextAccessor>(h => new HttpContextAccessor { HttpContext = context });

            ServiceProvider = services.BuildServiceProvider();

            SignInManager = ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
            Context = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            UserManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            SignInManagerMock = new Mock<SignInManager<ApplicationUser>>(UserManager,
                ServiceProvider.GetRequiredService<IHttpContextAccessor>(),
                ServiceProvider.GetRequiredService<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("http://google.com")]
        public void GetLoginTest(string returnUrl)
        {
            // Arrange
            var controller = new AccountController(Context, UserManager, SignInManager, LoggerMock.Object, ServiceProvider,
                EmailSenderMock.Object, StringLocalizerMock.Object);

            // Act
            var result = controller.Login(returnUrl);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.ViewData.Should().Contain("ReturnUrl", returnUrl);
        }

        [Fact]
        public async void LoginSuccessTest()
        {
            const string email = "test@test.test";
            const string password = "test1A!";

            // Arrange
            SignInManagerMock.Setup(o => o.PasswordSignInAsync(email, password, false, false))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));
            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.Setup(o => o.IsLocalUrl(null)).Returns(false);

            var controller = new AccountController(Context, UserManager, SignInManagerMock.Object,
                ServiceProvider.GetRequiredService<ILoggerFactory>(), ServiceProvider,
                EmailSenderMock.Object, StringLocalizerMock.Object)
            {
                Url = urlHelperMock.Object
            };

            var model = new LoginViewModel
            {
                Email = email,
                Password = password
            };

            // Act
            var result = await controller.Login(model);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>().Which.ControllerName.Should().Be("Home");
            result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Index");
        }

        [Fact]
        public async void LoginFailureTest()
        {
            const string email = "test@test.test";
            const string password = "test1A!";

            // Arrange
            SignInManagerMock.Setup(
                o => o.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Failed));

            var controller = new AccountController(Context, UserManager, SignInManagerMock.Object,
                ServiceProvider.GetRequiredService<ILoggerFactory>(), ServiceProvider,
                EmailSenderMock.Object, StringLocalizerMock.Object);

            var model = new LoginViewModel
            {
                Email = email,
                Password = password
            };

            // Act
            var result = await controller.Login(model);

            // Assert
            result.Should().BeOfType<ViewResult>().Which.ViewData.ModelState.Count.Should().Be(1);
        }
    }
}
