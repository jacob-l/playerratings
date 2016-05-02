using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PlayerRatings.Localization
{
    public class CustomStringLocalizer : IStringLocalizer
    {
        private readonly string _preferedLang;

        public CustomStringLocalizer(string preferedLang)
        {
            _preferedLang = preferedLang;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            return new List<LocalizedString>();
        }

        LocalizedString IStringLocalizer.this[string name] => GetLocalizedString(name);

        LocalizedString IStringLocalizer.this[string name, params object[] arguments]
            => new LocalizedString(name, string.Format(GetLocalizedString(name), arguments));

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private LocalizedString GetLocalizedString(string name)
        {
            return new LocalizedString(name, LocalizationKey.GetLocalization(name, _preferedLang));
        }
    }
}
