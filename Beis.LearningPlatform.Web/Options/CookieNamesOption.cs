namespace Beis.LearningPlatform.Web.Options
{
    public class CookieNamesOption
    {
        internal const string CookieNames = "CookieNamesConfig";

        public Essential Essential { get; set; }

        public NonEssential NonEssential { get; set; }
    }

    public class Essential
    {
        public string HtGcookieAcceptedCookie { get; set; }

        public string HtGRememberSettingsCookie { get; set; }

        public string HtGJSEnabledCookie { get; set; }
    }

    public class NonEssential
    {
        public string HtGAnalyticsCookie { get; set; }

        public string HtGMarketingCookie { get; set; }
    }
}