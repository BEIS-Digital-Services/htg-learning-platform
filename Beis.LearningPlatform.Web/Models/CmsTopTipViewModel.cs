namespace Beis.LearningPlatform.Web.Models
{
    public class CmsTopTipViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsTopTipViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get {
                return !string.IsNullOrWhiteSpace(_cmsPageComponent.copy) && !string.IsNullOrWhiteSpace(_cmsPageComponent.header);
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

        public string HtmlCopy { get; set; }
    }
}