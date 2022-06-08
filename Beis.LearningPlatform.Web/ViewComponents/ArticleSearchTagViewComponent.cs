using Microsoft.Extensions.Primitives;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class ArticleSearchTagViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticleSearchTagViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke(IList<CMSSearchTag> searchTags)
        {
            var yourTags = StringValues.Empty;
            _httpContextAccessor.HttpContext?.Request.Query.TryGetValue("yourTags", out yourTags);
            return View(new ArticleSearchTagViewModel
            {
                Tags = searchTags,
                YourTags = yourTags
            });
        }
    }
}