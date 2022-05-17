using Beis.LearningPlatform.Web.StrapiApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beis.LearningPlatform.Web.Models
{
    public class CmsImageItemViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsImageItemViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent;
        }

        public bool HasContent
        {
            get
            {
                return !string.IsNullOrEmpty(_cmsPageComponent.header)
                    && !string.IsNullOrEmpty(_cmsPageComponent.Summary)
                    && _cmsPageComponent.image != null
                    && _cmsPageComponent.Link != null;
            }
        }

        public string Header
        {
            get
            {
                return _cmsPageComponent.header;
            }
        }

        public string Summary
        {
            get
            {
                return _cmsPageComponent.Summary;
            }
        }

        public CMSPageImage Image
        {
            get
            {
                return _cmsPageComponent.image;
            }
        }

        public CMSPageLink Link
        {
            get
            {
                return _cmsPageComponent.Link;
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }
    }
}