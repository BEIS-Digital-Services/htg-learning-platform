namespace Beis.LearningPlatform.Web.Models
{
    public class ImageWithTextViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public ImageWithTextViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get {
                return !string.IsNullOrWhiteSpace(_cmsPageComponent.copy) && _cmsPageComponent.image != null;
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

        public string ImageUrl { get; set; }
        public string HtmlCopy { get; set; }
		public string ImageAlt { get; set; }

        public string ImageCaption
        {
            get
            {
                if (!string.IsNullOrEmpty(_cmsPageComponent.CaptionType) && (Enum.TryParse(_cmsPageComponent.CaptionType, out CaptionTypes captionType)))
                {
                    if (captionType == CaptionTypes.SpecificCaption)
                        return _cmsPageComponent.ImageCaption;
                    else if (captionType == CaptionTypes.ImageLibCaption)
                        return _cmsPageComponent.image.caption;
                }
                return String.Empty;
            }
        }
    }
}