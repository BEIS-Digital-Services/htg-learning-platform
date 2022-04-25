using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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