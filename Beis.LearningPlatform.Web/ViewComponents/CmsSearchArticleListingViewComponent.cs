namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsSearchArticleListingViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICmsService _cmsService;

        public CmsSearchArticleListingViewComponent(IHttpContextAccessor httpContextAccessor, ICmsService cmsService)
        {
            _httpContextAccessor = httpContextAccessor;
            _cmsService = cmsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsSearchArticleListingViewModel(cmsPageComponent);
            
            // Get the articles selected for the component in the CMS.
            // Note this is a slimmed down version of the Search-Article model, does not have the full display properties.
            var searchArticleIds = viewModel.GetSearchArticleIds(); 

            // Using the ids of the selected articles - get the full articles (including tags, description properties) from the CMS API
            var fullSearchArticles = (await _cmsService.GetSearchArticles(searchArticleIds, true)).ToList(); // To list as we will be using > once

            // Get any selected tags from the querystring (we are on a CMS page)
            viewModel.SelectedTagIds = GetSelectedTagIds().ToList(); // To list, will also be called from the view

            // Then filter the full article list by tags (API)
            if (viewModel.SelectedTagIds?.Any() == true)
            {
                fullSearchArticles = fullSearchArticles 
                    .Where(x => viewModel.SelectedTagIds.Intersect(x.tags.Select(x => x.id)).Any())
                    .ToList();
            }   
            
            // Set articles and tags on the viewmodel
            viewModel.FullSearchArticles = fullSearchArticles;
            viewModel.DistinctTags = fullSearchArticles
                .SelectMany(x => x.tags)
                .GroupBy(x => x.id).Select(x => x.First()) // All tags present in the selected articles, distinct by id
                .OrderBy(x => x.displayName);

            return View(viewModel);
        }

        private IEnumerable<int> GetSelectedTagIds()
        {
            var query = _httpContextAccessor.HttpContext?.Request?.Query;
            if (query == null || !query.ContainsKey("tag-ids"))
            {
                return Enumerable.Empty<int>();
            }

            return query["tag-ids"].ToString()
                .Split(',').Select(x => int.Parse(x));
        }
    }
}