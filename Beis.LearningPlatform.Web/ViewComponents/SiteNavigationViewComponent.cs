using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Beis.LearningPlatform.Web.ViewComponents
{

	public class SiteNavigationViewComponent : ViewComponent
    {
		private readonly ICmsService _cmsService;

		public SiteNavigationViewComponent(ICmsService cmsService)
        {
            _cmsService = cmsService;
        }

        public IViewComponentResult Invoke(IPageViewModel pageViewModel)
        {
            var viewModel = new SiteNavigationViewModel(pageViewModel);
            return View(viewModel);
        }
    }
}