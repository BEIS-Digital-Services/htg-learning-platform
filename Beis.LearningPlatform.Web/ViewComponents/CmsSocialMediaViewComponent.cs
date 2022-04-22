using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Web;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsSocialMediaViewComponent : ViewComponent
    {
		private readonly PathString _path;
		private readonly string _baseUrl;

		public CmsSocialMediaViewComponent(IHttpContextAccessor httpContextAccessor, IOptions<WebsiteOption> websiteOptions)
        {
            _path = httpContextAccessor.HttpContext?.Request?.Path ?? throw new ArgumentException("Error getting HttpContext.Request.Path", nameof(httpContextAccessor));
            _baseUrl = websiteOptions.Value?.BaseUrl ?? throw new ArgumentException("Error getting BaseUrl", nameof(websiteOptions));
        }

        public IViewComponentResult Invoke(IPageViewModel pageViewModel, CMSPageComponent cmsPageComponent)
        {
			var viewModel = new CmsSocialMediaViewModel(cmsPageComponent)
			{
				ShareTitle = HttpUtility.UrlEncode(string.IsNullOrWhiteSpace(cmsPageComponent?.shareLinkTitle) ? pageViewModel?.pageTitle : cmsPageComponent.shareLinkTitle),
				ShareLink = string.IsNullOrWhiteSpace(cmsPageComponent?.shareLink) ? string.Concat(_baseUrl.TrimEnd('/'), _path) : cmsPageComponent.shareLink
			};
			return View(viewModel);
        }
    }
}