using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            _httpContextAccessor.HttpContext?.Request.Query.TryGetValue("yourTags", out var yourTags);
            return View(new ArticleSearchTagViewModel
            {
                Tags = searchTags,
                YourTags = yourTags
            });
        }
    }
}