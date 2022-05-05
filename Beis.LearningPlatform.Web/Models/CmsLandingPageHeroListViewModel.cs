using Beis.LearningPlatform.Web.StrapiApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beis.LearningPlatform.Web.Models
{
    public class CmsLandingPageHeroListViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsLandingPageHeroListViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get
            {
                return Heroes?.Any(x => x.HasContent) ?? false;
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

        public List<CmsLandingPageHeroViewModel> Heroes {
            get {
                return _cmsPageComponent.Heroes;
            }
        }
    }
}