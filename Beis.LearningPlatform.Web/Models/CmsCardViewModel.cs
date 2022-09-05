namespace Beis.LearningPlatform.Web.Models
{
    public class CmsCardViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsCardViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent;
        }
        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
        
        public string OneTitle { get; set; }
        public string TwoTitle { get; set; }
        public string ThreeTitle { get; set; }
        public string FourTitle { get; set; }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }
    }
}