namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class FullSearchArticleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CMSSearchArticle fullSearchArticle)
        {
            return View(fullSearchArticle);
        }
    }
}
