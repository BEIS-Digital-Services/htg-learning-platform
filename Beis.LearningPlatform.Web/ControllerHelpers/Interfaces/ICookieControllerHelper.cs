namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface ICookieControllerHelper : ICmsControllerHelperBase
    {
        ControllerHelperOperationResponse ProcessCookie(string cookieType, bool isAccept);
        ControllerHelperOperationResponse SaveCookiesPreferences(SaveCookiePreferenceModel saveCookiePreferenceModel);
        ControllerHelperOperationResponse<UserCookiePreferencesModel> GetUserCookiePreferences();
        bool SafeRedirectToReferer(out string redirectUrl);
    }
}