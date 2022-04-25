using Beis.LearningPlatform.Web.StrapiApi.Models;
using System;

namespace Beis.LearningPlatform.Web.Models
{
    public class CmsBusinessCardViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsBusinessCardViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get {
                return !string.IsNullOrWhiteSpace(_cmsPageComponent.businessName) && _cmsPageComponent.image != null;
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

        public string VisitUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}