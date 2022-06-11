using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace PlayerRatings.Localization
{
    public class CustomStringLocalizerFactory : IStringLocalizerFactory, ILanguageData
    {
        public const string DefaultLanguage = "en";

        private readonly IHttpContextAccessor _contextAccessor;

        public CustomStringLocalizerFactory(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new CustomStringLocalizer(GetLanguage(ValidLanguages));
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new CustomStringLocalizer(GetLanguage(ValidLanguages));
        }

        private string GetLanguage(IEnumerable<string> validLanguages)
        {
            if (_contextAccessor.HttpContext.Request == null)
            {
                return DefaultLanguage;
            }

            var langCookie = _contextAccessor.HttpContext.Request.Cookies[CookieName];
            var lang = string.IsNullOrEmpty(langCookie) || !ValidLanguages.Contains(langCookie)
                ? CultureInfo.CurrentCulture.TwoLetterISOLanguageName
                : langCookie;

            return validLanguages.Contains(lang) ? lang : DefaultLanguage;
        }

        public IEnumerable<string> ValidLanguages => new [] { "en", "ru" };

        public string CurrentLanguage => GetLanguage(ValidLanguages);

        public string CookieName => "lang";
    }
}
