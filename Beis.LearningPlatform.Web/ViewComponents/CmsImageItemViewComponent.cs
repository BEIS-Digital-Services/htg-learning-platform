﻿using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsImageItemViewComponent : ViewComponent
    {
        public CmsImageItemViewComponent()
        {
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsImageItemViewModel(cmsPageComponent);
            return View(viewModel);
        }
    }
}