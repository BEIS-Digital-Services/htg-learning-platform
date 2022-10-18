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

        public bool HasContent
        {
            get
            {
                return !string.IsNullOrEmpty(Header)
                    && !string.IsNullOrEmpty(Intro);
            }
        }
    }
}