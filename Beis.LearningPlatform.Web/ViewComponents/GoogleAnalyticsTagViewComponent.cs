using Beis.LearningPlatform.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class GoogleAnalyticsTagViewComponent : ViewComponent
    {
        private readonly ICookieService _cookieService;

        public GoogleAnalyticsTagViewComponent(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = _cookieService.GetUserCookiePreferences();
            return View(viewModel);
        }
    }
}