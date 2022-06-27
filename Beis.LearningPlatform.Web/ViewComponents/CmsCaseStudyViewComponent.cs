namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsCaseStudyViewComponent : ViewComponent
    {
        private readonly CmsOption _cmsOption;
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsCaseStudyViewComponent(IOptions<CmsOption> cmsOptions, MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
            _cmsOption = cmsOptions.Value;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            
            var viewModel = new CmsCaseStudyViewModel(cmsPageComponent);
            if (viewModel.HasContent)
            {
                viewModel.HtmlCopy = Markdown.ToHtml(viewModel.Component.content.copy, _markdownPipeline);
                if (viewModel.Component.image != null)
                {
                    viewModel.ImageUrl = $"{_cmsOption.ApiBaseUrl}{viewModel.Component.image.url}";
                }
            }
            return View(viewModel);
        }
    }
}