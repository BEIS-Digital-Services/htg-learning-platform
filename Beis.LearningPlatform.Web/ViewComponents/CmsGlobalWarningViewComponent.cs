namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsGlobalWarningViewComponent : ViewComponent
    {
        private readonly ICmsService _cmsService;
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsGlobalWarningViewComponent(ICmsService cmsService, MarkdownPipeline markdownPipeline)
        {
            _cmsService = cmsService;
            _markdownPipeline = markdownPipeline;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPageViewModel pageViewModel)
        {
            var pagename = (pageViewModel?.pagename ?? string.Empty).Trim();
            if (pagename.Equals("home", StringComparison.OrdinalIgnoreCase))
            {
                return View(Enumerable.Empty<GlobalWarningMessageModel>());
            }

            var viewModel = await _cmsService.GetGlobalWarningMessages();
            foreach (var globalWarning in viewModel.Where(x => !string.IsNullOrEmpty(x.Body)))
            { 
                globalWarning.BodyHtml = Markdown.ToHtml(globalWarning.Body, _markdownPipeline);
            }

            return View(viewModel);
        }
    }
}