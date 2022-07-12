namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsExpressionOfInterestViewComponent : ViewComponent
    {        
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsExpressionOfInterestViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }


        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsExpressionOfInterestViewModel(cmsPageComponent);

            if (viewModel.HasContent)
            {
                viewModel.FormIntroHtml = Markdown.ToHtml(cmsPageComponent.FormIntro, _markdownPipeline);
                viewModel.ThankYouTextHtml = Markdown.ToHtml(cmsPageComponent.ThankYouText, _markdownPipeline);
            }

            return View(viewModel);
        }
    }
}