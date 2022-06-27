namespace Beis.LearningPlatform.Web.Models
{
    public class CmsThreeLevelsViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsThreeLevelsViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_cmsPageComponent.header1)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.header2)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.header3)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.copy1)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.copy2)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.copy3);
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

        public string HtmlCopy1 { get; set; }        
        public string HtmlCopy2 { get; set; }        
        public string HtmlCopy3 { get; set; }
    }
}