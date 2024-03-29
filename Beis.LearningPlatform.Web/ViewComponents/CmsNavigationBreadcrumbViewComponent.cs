﻿namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsNavigationBreadcrumbViewComponent : ViewComponent
    {
		private readonly ICmsService _cmsService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CmsNavigationBreadcrumbViewComponent(ICmsService cmsService, IHttpContextAccessor httpContextAccessor)
        {
            _cmsService = cmsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPageViewModel pageViewModel, CMSPageComponent cmsPageComponent)
        {
			var siteNavigationViewModel = new SiteNavigationViewModel(pageViewModel, _httpContextAccessor.HttpContext?.Request?.Path)
			{
				SiteNavigationModels = await _cmsService.GetSiteNavigation()
			};
            siteNavigationViewModel.SetActiveNavigationModel();


            var viewModel = new NavigationBreadcrumbViewModel(cmsPageComponent)
            {
                SiteNavigationModel = siteNavigationViewModel
            };

            return View(viewModel);
        }
    }
}