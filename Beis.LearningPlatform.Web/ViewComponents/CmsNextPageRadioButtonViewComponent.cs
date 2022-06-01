using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsNextPageRadioButtonViewComponent : ViewComponent
    {
        public CmsNextPageRadioButtonViewComponent()
        {
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsNextPageRadioButtonViewModel(cmsPageComponent);
            return View(viewModel);
        }
    }
}