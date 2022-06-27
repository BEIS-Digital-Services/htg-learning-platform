namespace Beis.LearningPlatform.Web.Models
{
    public class CmsQuoteTwoThirdsColumnViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsQuoteTwoThirdsColumnViewModel(CMSPageComponent cmsPageComponent)
        {
            
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_cmsPageComponent.quote);
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

        public string HtmlQuote { get; set; }

        public bool HasQuoteTwoThirdsColumnAuthor
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_cmsPageComponent.quoteTwoThirdsAuthor);
            }
        }
    }
}