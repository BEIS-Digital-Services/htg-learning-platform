using Beis.LearningPlatform.Web.StrapiApi.Models;
using System;

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
	}
}