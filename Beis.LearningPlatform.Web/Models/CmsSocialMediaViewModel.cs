namespace Beis.LearningPlatform.Web.Models
{
    public class CmsSocialMediaViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsSocialMediaViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent;
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

		public string ShareTitle { get; set; }
		public string ShareLink { get; set; }
	}
}