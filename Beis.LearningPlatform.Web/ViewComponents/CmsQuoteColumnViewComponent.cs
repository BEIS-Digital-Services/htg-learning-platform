namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsQuoteColumnViewComponent : ViewComponent
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsQuoteColumnViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsQuoteColumnViewModel(cmsPageComponent);
            if (viewModel.HasContent)
            {
                viewModel.HtmlQuote = Markdown.ToHtml(viewModel.Component.quote, _markdownPipeline);
                viewModel.HtmlCopy = Markdown.ToHtml(viewModel.Component.copy, _markdownPipeline);
                viewModel.QuoteAriaText = viewModel.Component.aria;
            }
            return View(viewModel);
        }
    }
}