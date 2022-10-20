namespace Beis.LearningPlatform.Web.Models
{
    public class CmsHomePageWarningViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsHomePageWarningViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent;
        }

        public string Header { get { return _cmsPageComponent?.header; } }
        public string Intro { get { return _cmsPageComponent?.intro; } }
        public string IntroHtml { get; set; }
        public CMSSimpleLink CTALink { get { return _cmsPageComponent?.CTALink; } }

        public bool HasContent
        {
            get
            {
                return !string.IsNullOrEmpty(Header)
                    && !string.IsNullOrEmpty(Intro);
            }
        }

        public bool Hide
        {
            get
            {
                return _cmsPageComponent?.hide ?? false;
            }
        }
    }
}