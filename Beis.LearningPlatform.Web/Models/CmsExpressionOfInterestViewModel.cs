namespace Beis.LearningPlatform.Web.Models
{
    public class CmsExpressionOfInterestViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsExpressionOfInterestViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent;
        }

        public bool HasContent
        {
            get
            {
                return !string.IsNullOrEmpty(this.CTABannerText)
                    && !string.IsNullOrEmpty(this.CTAButtonText)
                    && !string.IsNullOrEmpty(this.FormHeader)
                    && !string.IsNullOrEmpty(this.FormIntro)
                    && !string.IsNullOrEmpty(this.ThankYouHeader)
                    && !string.IsNullOrEmpty(this.ThankYouText);
            }
        }

        public string CTABannerText { get { return _cmsPageComponent.CTABannerText; } }
        public string CTAButtonText { get { return _cmsPageComponent.CTAButtonText; } }
        public string FormHeader { get { return _cmsPageComponent.FormHeader; } }
        public string FormIntro { get { return _cmsPageComponent.FormIntro; } }
        public string FormIntroHtml { get; set; }
        public string ThankYouHeader { get { return _cmsPageComponent.ThankYouHeader; } }
        public string ThankYouText { get { return _cmsPageComponent.ThankYouText; } }
        public string ThankYouTextHtml { get; set; }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

    }
}