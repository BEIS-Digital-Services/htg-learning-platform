namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsTwoColumnTextViewComponent : ViewComponent
    {
        public CmsTwoColumnTextViewComponent()
        {
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsTwoColumnTextViewModel(cmsPageComponent);
            return View(viewModel);
        }
    }
}