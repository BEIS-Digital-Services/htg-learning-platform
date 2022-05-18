using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beis.LearningPlatform.Web.ViewComponents
{
	public class CmsLandingPageHeroBannerViewComponent : ViewComponent
    {
        public CmsLandingPageHeroBannerViewComponent()
        {
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsLandingPageHeroBannerViewModel(cmsPageComponent);
            return View(viewModel);
        }
    }
}