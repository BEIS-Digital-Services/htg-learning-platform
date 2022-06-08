using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class FullSearchArticleViewComponent : ViewComponent
    {
        public FullSearchArticleViewComponent() { }

        public IViewComponentResult Invoke(CMSSearchArticle fullSearchArticle)
        {
            return View(fullSearchArticle);
        }
    }
}
