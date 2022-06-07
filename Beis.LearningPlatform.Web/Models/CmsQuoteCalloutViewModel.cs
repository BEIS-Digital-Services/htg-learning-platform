namespace Beis.LearningPlatform.Web.Models
{
    public class CmsQuoteCalloutViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsQuoteCalloutViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_cmsPageComponent.copy)
                    && _cmsPageComponent.quotePosition.HasValue
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.quote);
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
        public string HtmlCopy { get; set; }

        public bool HasQuoteCalloutAuthor
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_cmsPageComponent.quoteCalloutAuthor);
            }
        }
    }
}