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