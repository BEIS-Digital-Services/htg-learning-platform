namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsLinkListHeroViewComponent : ViewComponent
    {
        
        public CmsLinkListHeroViewComponent(){ }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsLinkListHeroViewModel(cmsPageComponent);
            return View(viewModel);
        }
    }
}