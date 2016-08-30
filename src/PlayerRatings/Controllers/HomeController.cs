using System;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.OptionsModel;
using PlayerRatings.Localization;
using PlayerRatings.ViewModels.Home;
using PlayerRatings.Models;
using PlayerRatings.Services;

namespace PlayerRatings.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ILanguageData _languageData;
        private readonly IOptions<AppSettings> _settings;
        private readonly IEmailSender _emailSender;

        public HomeController(
            ApplicationDbContext context,
            IStringLocalizer<HomeController> localizer,
            ILanguageData languageData,
            IOptions<AppSettings> settings,
            IEmailSender emailSender)
        {
            _context = context;
            _localizer = localizer;
            _languageData = languageData;
            _settings = settings;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel(_context.League.Count(), _context.Users.Count(), _context.Match.Count());

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(_settings.Value.ContactEmail, "Message from support page",
                    model.Message + "\n\n\n" + model.ClientContact);

                ViewData["Message"] = _localizer[nameof(LocalizationKey.YourMessageIsSent)];

                return View();
            }

            return View(model);
        }

        public IActionResult Date()
        {
            return Content(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeLanguage(string language, string curUrl)
        {
            Response.Cookies.Append(_languageData.CookieName, language);

            return Redirect(curUrl);
        }
    }
}
