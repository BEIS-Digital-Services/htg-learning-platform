namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsCardViewComponent : ViewComponent
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsCardViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsCardViewModel(cmsPageComponent);
            viewModel.One = Markdown.ToHtml(viewModel.Component.one, _markdownPipeline);
            viewModel.Two = Markdown.ToHtml(viewModel.Component.two, _markdownPipeline);
            viewModel.Three = Markdown.ToHtml(viewModel.Component.three, _markdownPipeline);
            viewModel.Four = Markdown.ToHtml(viewModel.Component.four, _markdownPipeline);
            viewModel.OneTitle = viewModel.Component.OneTitle;
            viewModel.TwoTitle = viewModel.Component.TwoTitle;
            viewModel.ThreeTitle = viewModel.Component.ThreeTitle;
            viewModel.FourTitle = viewModel.Component.FourTitle;
            return View(viewModel);
        }
    }
}