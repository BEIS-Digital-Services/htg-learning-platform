namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsQuoteCalloutViewComponent : ViewComponent
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsQuoteCalloutViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsQuoteCalloutViewModel(cmsPageComponent);
            if (viewModel.HasContent)
            {
                viewModel.HtmlQuote = Markdown.ToHtml(viewModel.Component.quote, _markdownPipeline);
                viewModel.HtmlCopy = Markdown.ToHtml(viewModel.Component.copy, _markdownPipeline);
            }
            return View(viewModel);
        }
    }
}