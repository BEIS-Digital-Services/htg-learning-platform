namespace Beis.LearningPlatform.Web.Models
{
    public class CmsSearchArticleListingViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsSearchArticleListingViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent;
        }

        public bool HasContent
        {
            get
            {
                return !string.IsNullOrEmpty(this.Header)
                    && (SearchArticles?.Any(x => x.HasContent) == true);
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

        internal IEnumerable<int> GetSearchArticleIds()
        {
            return SearchArticles?.Select(x => x.id);
        }

        public IEnumerable<CMSSearchArticle> SearchArticles
        {
            get
            {
                return _cmsPageComponent.SearchArticles?
                    .Where(x => x?.HasContent == true)
                    .Select(x => x.SearchArticle);
            }
        }

        public string Header
        {
            get
            {
                return _cmsPageComponent.header;
            }
        }

        /// <summary>
        /// The strapi cms relation type gives us a reduced set of properties in the "_cmsPageComponent.SearchArticles" property.
        /// Using Id's the full search article properties is assigned here.
        /// </summary>
        public IEnumerable<CMSSearchArticle> FullSearchArticles { get; set; }
        
        /// <summary>
        /// Distinct tags - assigned from the FullSearchArticles tags property
        /// </summary>
        public IEnumerable<CMSSearchTag> DistinctTags { get; set; }
        public List<int> SelectedTagIds { get; set; }

        public bool IsTagSelected(CMSSearchTag tag)
        {
            return SelectedTagIds?.Contains(tag.id) == true;
        }
        public string PageName { get; set; }
    }
}