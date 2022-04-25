using Beis.LearningPlatform.Web.StrapiApi.Models;
using System;

namespace Beis.LearningPlatform.Web.Models
{
    public class CmsQuoteColumnViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;
        private readonly bool _hasContent;
        private readonly bool _hasQuoteColumnAuthor;

        public CmsQuoteColumnViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
            _hasContent = !string.IsNullOrWhiteSpace(_cmsPageComponent.copy)
                    && _cmsPageComponent.quotePosition.HasValue
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.quote);
            _hasQuoteColumnAuthor = !string.IsNullOrWhiteSpace(_cmsPageComponent.quoteColumnAuthor);
        }

        public bool HasContent
        {
            get
            {
                return _hasContent;
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
        public string QuoteAriaText { get; set; }


        public bool HasQuoteColumnAuthor
        {
            get
            {
                return _hasQuoteColumnAuthor;
            }
        }
    }
}