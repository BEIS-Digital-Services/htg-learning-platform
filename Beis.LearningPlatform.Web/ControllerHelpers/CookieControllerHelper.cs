namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class CookieControllerHelper : CmsControllerHelperBase, ICookieControllerHelper
    {
        private readonly ICookieService _cookieService;

        public CookieControllerHelper(ILogger<HomeControllerHelper> logger,
                              ICmsService cmsService,
                              ICmsFeedbackService cmsFeedbackService,
                              ICookieService cookieService,
                              IOptions<CmsOption> cmsOptions,
                              IHttpContextAccessor httpContextAccessor
            ) : base(logger, cmsOptions, cmsService, cmsFeedbackService, httpContextAccessor)
        {
            _cookieService = cookieService;
        }

        public ControllerHelperOperationResponse ProcessCookie(string cookieType, bool isAccept)
        {
            var requestId = RecordRequest();
            _cookieService.ProcessCookie(cookieType, isAccept);
            return new ControllerHelperOperationResponse(requestId);
        }

        public ControllerHelperOperationResponse SaveCookiesPreferences(SaveCookiePreferenceModel saveCookiePreferenceModel)
        {
            var requestId = RecordRequest();
            _cookieService.SaveCookiesPreferences(saveCookiePreferenceModel);
            return new ControllerHelperOperationResponse(requestId);
        }

        public ControllerHelperOperationResponse<UserCookiePreferencesModel> GetUserCookiePreferences()
        {
            var requestId = RecordRequest();
            var payload = _cookieService.GetUserCookiePreferences();
            return new ControllerHelperOperationResponse<UserCookiePreferencesModel>(requestId, payload);
        }

        public bool SafeRedirectToReferer(out string redirectUrl)
        {
            redirectUrl = default;
            var urlReferer = _httpContextAccessor.HttpContext?.Request.Headers["Referer"].ToString();

            if (string.IsNullOrWhiteSpace(urlReferer))
            {
                _logger.LogWarning($"{nameof(CookieControllerHelper)}: string.IsNullOrWhiteSpace(urlReferer)");
                return false;
            }

            var urlKind = urlReferer.StartsWith("http") ? UriKind.Absolute : UriKind.Relative;
            if (!Uri.TryCreate(urlReferer, urlKind, out Uri redirectUri))
            {
                _logger.LogWarning($"{nameof(CookieControllerHelper)}: !Uri.TryCreate({urlReferer})");
                return false;
            }

            redirectUrl = redirectUri.PathAndQuery; // Only redirect into our domain
            return true;
        }
    }
}