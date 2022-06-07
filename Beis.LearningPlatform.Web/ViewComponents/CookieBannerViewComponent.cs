namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CookieBannerViewComponent : ViewComponent
    {
        private readonly ICookieService _cookieService;

        public CookieBannerViewComponent(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new CookieBannerViewModel
            {
                UserCookiePreferences = _cookieService.GetUserCookiePreferences(),
                ActionName = GetRouteData("action"),
                ControllerName = GetRouteData("controller")
            };

            return View(viewModel);
        }

        private string GetRouteData(string key)
        {
            if (!ViewContext?.RouteData?.Values?.ContainsKey(key) ?? false)
            {
                return null;
            }

            return ViewContext.RouteData.Values[key].ToString();
        }
    }
}