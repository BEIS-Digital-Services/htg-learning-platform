﻿namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsLandingPageHeroViewComponent : ViewComponent
    {        
        public IViewComponentResult Invoke(CmsLandingPageHeroViewModel landingPageHero)
        {
            return View(landingPageHero);
        }
    }
}