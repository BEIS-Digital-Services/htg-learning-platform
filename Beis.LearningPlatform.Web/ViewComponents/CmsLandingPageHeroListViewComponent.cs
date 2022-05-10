using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beis.LearningPlatform.Web.ViewComponents
{
	public class CmsLandingPageHeroListViewComponent : ViewComponent
    {
        public CmsLandingPageHeroListViewComponent()
        {
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsLandingPageHeroListViewModel(cmsPageComponent);
            return View(viewModel);
        }
    }
}