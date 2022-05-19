using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ViewComponents
{
	public class CmsSearchArticleListingViewComponent : ViewComponent
    {
        private readonly ICmsService _cmsService;

        public CmsSearchArticleListingViewComponent(ICmsService cmsService)
        {
            _cmsService = cmsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsSearchArticleListingViewModel(cmsPageComponent);
            var searchArticleIds = viewModel.GetSearchArticleIds();
            viewModel.FullSearchArticles = await _cmsService.GetSearchArticles(searchArticleIds, true);

            return View(viewModel);
        }
     
    }
}