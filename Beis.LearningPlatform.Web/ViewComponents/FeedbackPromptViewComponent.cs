namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class FeedbackPromptViewComponent : ViewComponent
    {
        private readonly ICookieService _cookieService;

        public FeedbackPromptViewComponent(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new FeedbackPromptViewModel
            {
                IsJavascriptEnabled = _cookieService.GetUserCookiePreferences()?.IsJavascriptEnabled,
                CmsPageComponent = cmsPageComponent
            };
            return View(viewModel);
        }
    }
}