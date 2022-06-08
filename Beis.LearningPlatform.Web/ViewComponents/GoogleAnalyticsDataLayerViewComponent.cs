namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class GoogleAnalyticsDataLayerViewComponent : ViewComponent
    {		
        public IViewComponentResult Invoke(IPageViewModel pageViewModel)
        {
            return View(pageViewModel);
        }
    }
}