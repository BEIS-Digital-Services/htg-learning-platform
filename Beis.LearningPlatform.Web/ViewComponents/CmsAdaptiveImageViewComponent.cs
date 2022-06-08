namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsAdaptiveImageViewComponent : ViewComponent
	{
		private readonly string _cmsBaseUrl;

		public CmsAdaptiveImageViewComponent(IOptions<CmsOption> cmsOptions)
		{
			_cmsBaseUrl = cmsOptions.Value.ApiBaseUrl;
		}

		public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
		{
			var viewModel = new CmsAdaptiveImageViewModel(cmsPageComponent, _cmsBaseUrl);
			return View(viewModel);
		}
	}
}