namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsTopTipViewComponent : ViewComponent
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsTopTipViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsTopTipViewModel(cmsPageComponent);
            if (viewModel.HasContent)
            {
                viewModel.HtmlCopy = Markdown.ToHtml(viewModel.Component.copy, _markdownPipeline);
            }
            return View(viewModel);
        }
    }
}