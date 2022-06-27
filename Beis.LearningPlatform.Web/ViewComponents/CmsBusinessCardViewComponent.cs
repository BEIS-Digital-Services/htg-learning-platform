namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsBusinessCardViewComponent : ViewComponent
    {
        private readonly CmsOption _cmsOption;

        public CmsBusinessCardViewComponent(IOptions<CmsOption> cmsOptions)
        {
            _cmsOption = cmsOptions.Value;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsBusinessCardViewModel(cmsPageComponent);
            if (viewModel.HasContent)
            {
                viewModel.ImageUrl = $"{_cmsOption.ApiBaseUrl}{viewModel.Component.image.url}";
            }

            var visitUrl = viewModel.Component.visit;
            if (!string.IsNullOrWhiteSpace(visitUrl))
            {
                viewModel.VisitUrl = visitUrl.StartsWith("http") ? visitUrl : $"//{visitUrl}";
            }

            return View(viewModel);
        }
    }
}