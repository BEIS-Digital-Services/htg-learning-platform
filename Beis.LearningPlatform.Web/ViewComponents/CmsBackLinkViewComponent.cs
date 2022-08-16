namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsBackLinkViewComponent : ViewComponent
    {
        
        public CmsBackLinkViewComponent(){ }

        public IViewComponentResult Invoke(IPageViewModel pageViewModel, CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsBackLinkViewModel(pageViewModel, cmsPageComponent);
            return View(viewModel);
        }
    }
}