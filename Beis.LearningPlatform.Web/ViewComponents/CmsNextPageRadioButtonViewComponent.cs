namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsNextPageRadioButtonViewComponent : ViewComponent
    {
        public CmsNextPageRadioButtonViewComponent()
        {
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsNextPageRadioButtonViewModel(cmsPageComponent);
            return View(viewModel);
        }
    }
}