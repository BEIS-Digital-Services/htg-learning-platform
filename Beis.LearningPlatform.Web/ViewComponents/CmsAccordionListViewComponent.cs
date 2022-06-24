namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsAccordionListViewComponent : ViewComponent
    {
        public CmsAccordionListViewComponent()
        {
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsAccordionListViewModel(cmsPageComponent);
            return View(viewModel);
        }
    }
}