namespace Beis.LearningPlatform.Web.Interfaces
{
    /// <summary>
    /// Serialise all settings into one json cookie?
    /// </summary>
    public interface ICookieService
    {
        void CloseCookieBanner();
        void SetIsGaAccepted(bool accepted);
        void SetIsHtgAccepted(bool accepted);
        void SetIsJavascriptEnabled(bool enabled);
        void SetRememberSettings(bool remember);
        void SetMarketingAccepted(bool accepted);

        UserCookiePreferencesModel GetUserCookiePreferences();

        void ProcessCookie(string cookieType, bool isAccept);
        void SaveCookiesPreferences(SaveCookiePreferenceModel saveCookiePreferenceModel);
    }
}