namespace Beis.LearningPlatform.Web.Models
{
    public class CmsLandingPageHeroBannerViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsLandingPageHeroBannerViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent;
        }

        public bool HasContent
        {
            get
            {
                return _cmsPageComponent.image != null
                    && !string.IsNullOrEmpty(_cmsPageComponent.header)
                    && !string.IsNullOrEmpty(_cmsPageComponent.intro);
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

        public string Header
        {
            get
            {
                return _cmsPageComponent.header;
            }
        }

        public string Intro
        {
            get
            {
                return _cmsPageComponent.intro;
            }
        }

        public string ImageUrl
        {
            get
            {
                return _cmsPageComponent.image.url;
            }
        }
    }
}