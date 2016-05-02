using System.Collections.Generic;

namespace PlayerRatings.Localization
{
    public interface ILanguageData
    {
        IEnumerable<string> ValidLanguages { get; }

        string CurrentLanguage { get; }

        string CookieName { get; }
    }
}
