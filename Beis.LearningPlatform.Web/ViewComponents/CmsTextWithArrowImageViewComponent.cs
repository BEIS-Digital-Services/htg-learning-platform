namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsTextWithArrowImageViewComponent : ViewComponent
    {
        public CmsTextWithArrowImageViewComponent()
        {
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsTextWithArrowImageViewModel(cmsPageComponent);
            return View(viewModel);
        }
    }
}