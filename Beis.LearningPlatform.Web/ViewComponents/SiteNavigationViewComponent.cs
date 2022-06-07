namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class SiteNavigationViewComponent : ViewComponent
    {
		private readonly ICmsService _cmsService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SiteNavigationViewComponent(ICmsService cmsService, IHttpContextAccessor httpContextAccessor)
        {
            _cmsService = cmsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPageViewModel pageViewModel)
        {
			var viewModel = new SiteNavigationViewModel(pageViewModel, _httpContextAccessor.HttpContext?.Request?.Path)
			{
				SiteNavigationModels = await _cmsService.GetSiteNavigation()
			};

			viewModel.SetActiveNavigationModel();

            return View(viewModel);
        }
    }
}