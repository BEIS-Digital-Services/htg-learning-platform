﻿using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ViewComponents
{

	public class SiteNavigationViewComponent : ViewComponent
    {
		private readonly ICmsService _cmsService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SiteNavigationViewComponent(ICmsService cmsService, IHttpContextAccessor httpContextAccessor)
        {
            _cmsService = cmsService ?? throw new ArgumentNullException(nameof(cmsService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
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