namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsLandingPageHeroBannerViewComponent : ViewComponent
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsLandingPageHeroBannerViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsLandingPageHeroBannerViewModel(cmsPageComponent);
            if (viewModel.HasContent)
            {
                viewModel.IntroHtml = Markdown.ToHtml(cmsPageComponent.intro, _markdownPipeline);
            }
            return View(viewModel);
        }
    }
}