namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsQuoteTwoThirdsColumnViewComponent : ViewComponent
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsQuoteTwoThirdsColumnViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsQuoteTwoThirdsColumnViewModel(cmsPageComponent);
            if (viewModel.HasContent)
            {
                viewModel.HtmlQuote = Markdown.ToHtml(viewModel.Component.quote, _markdownPipeline);
            }
            return View(viewModel);
        }
    }
}