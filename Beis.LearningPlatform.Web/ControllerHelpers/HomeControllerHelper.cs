namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class HomeControllerHelper : CmsControllerHelperBase, IHomeControllerHelper
    {
        public HomeControllerHelper(ILogger<HomeControllerHelper> logger,
                ICmsService cmsService,
                ICmsFeedbackService cmsFeedbackService,
                IOptions<CmsOption> cmsOptions,
                IHttpContextAccessor httpContextAccessor
            ) : base(logger, cmsOptions, cmsService, cmsFeedbackService, httpContextAccessor) {}

        public async Task SetReactiveTagComponents(CMSPageViewModel viewModel)
        {
            var component = viewModel.components.Where(item => item.__component.Equals("page.reactive-tags", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (component != null)
            {
                var strapiAction = "search-articles" + (viewModel.PreviewSearchArticles == true ? "?_publicationState=preview" : String.Empty);
                var searchArticles = await ProcessGetSearchArticlesResult(strapiAction);
                component.search_articles = searchArticles.OrderBy(x => x.order).ToList();
            }
        }

        public IList<string> GetCurrentTags(string yourTags)
        {
            return (string.IsNullOrWhiteSpace(yourTags) ? Array.Empty<string>() : yourTags.Split(','))
                .Distinct()
                .ToList();
        }

        public IList<string> GetCurrentTags(string yourTags, string tag, bool removeTag)
        {
            var currentTags = string.IsNullOrWhiteSpace(yourTags) ? new() : yourTags.Split(',').Distinct().ToList();
            if (removeTag)
            {
                currentTags.Remove(tag);
            }
            else
            {
                currentTags.Add(tag);
            }

            return currentTags.Distinct().ToList(); // Page reloads on adding URLs -> distinct .. 
        }
    }
}