namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsHomePageWarningViewComponent : ViewComponent
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsHomePageWarningViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsHomePageWarningViewModel(cmsPageComponent);
            viewModel.IntroHtml = Markdown.ToHtml(viewModel.Intro, _markdownPipeline);

            return View(viewModel);
        }
    }
}