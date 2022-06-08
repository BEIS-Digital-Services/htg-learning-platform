namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsThreeLevelsViewComponent : ViewComponent
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsThreeLevelsViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsThreeLevelsViewModel(cmsPageComponent);
            if (viewModel.HasContent)
            {
                viewModel.HtmlCopy1 = Markdown.ToHtml(viewModel.Component.copy1, _markdownPipeline);
                viewModel.HtmlCopy2 = Markdown.ToHtml(viewModel.Component.copy2, _markdownPipeline);
                viewModel.HtmlCopy3 = Markdown.ToHtml(viewModel.Component.copy3, _markdownPipeline);
            }
            return View(viewModel);
        }
    }
}