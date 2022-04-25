namespace Beis.LearningPlatform.Web.Models
{
    public class CookieBannerViewModel
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public UserCookiePreferencesModel UserCookiePreferences { get; set; }
        public bool UserCookiePreferencesSet { get { return UserCookiePreferences != null && UserCookiePreferences.IsGaAccepted.HasValue; } } 

    }
}