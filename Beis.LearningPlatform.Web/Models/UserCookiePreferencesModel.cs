namespace Beis.LearningPlatform.Web.Models
{
    public class UserCookiePreferencesModel
    {
        public bool IsCookieBannerClosed { get; set; }
        public bool? IsGaAccepted { get; set; }
        public bool? IsHtgAccepted { get; set; }
        public bool? IsJavascriptEnabled { get; set; }
        public bool? RememberSettings { get; set; }
        public bool? MarketingAccepted { get; set; }
    }
}