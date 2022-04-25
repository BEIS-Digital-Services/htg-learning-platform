using Beis.LearningPlatform.Web.StrapiApi.Models;
using System;

namespace Beis.LearningPlatform.Web.Models
{
    public class CmsCaseStudyViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsCaseStudyViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get
            {
                return _cmsPageComponent.content != null;
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
        public string ImageUrl { get; set; }
        
    }
}