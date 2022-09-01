namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class FullSearchArticleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CMSSearchArticle fullSearchArticle, string pageName)
        {
            fullSearchArticle.PageName = pageName;
            return View(fullSearchArticle);
        }
    }
}
