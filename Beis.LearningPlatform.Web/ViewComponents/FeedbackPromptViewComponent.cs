using Microsoft.Extensions.Primitives;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class FeedbackPromptViewComponent : ViewComponent
    {
        private readonly ICookieService _cookieService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeedbackPromptViewComponent(ICookieService cookieService, IHttpContextAccessor httpContextAccessor)
        {
            _cookieService = cookieService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new FeedbackPromptViewModel
            {
                IsJavascriptEnabled = _cookieService.GetUserCookiePreferences()?.IsJavascriptEnabled,
                IsFeedbackSubmitted = IsFeedbackSubmitted(),
                CmsPageComponent = cmsPageComponent
            };
            return View(viewModel);
        }

        private bool IsFeedbackSubmitted()
        {
            StringValues feedbackSubmitted = default;
            _httpContextAccessor.HttpContext?.Request.Query.TryGetValue("feedback-submitted", out feedbackSubmitted);
            return feedbackSubmitted.Any();
        }
    }
}