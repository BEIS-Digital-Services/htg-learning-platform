namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsThreeColumnHeroesViewComponent : ViewComponent
    {
        
        public CmsThreeColumnHeroesViewComponent(){ }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsThreeColumnHeroesViewModel(cmsPageComponent);
            return View(viewModel);
        }
    }
}