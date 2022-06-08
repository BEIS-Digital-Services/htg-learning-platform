using Beis.LearningPlatform.Web.StrapiApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beis.LearningPlatform.Web.Models
{
    public class CmsNextPageRadioButtonViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsNextPageRadioButtonViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent;
        }

        public bool HasContent
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_cmsPageComponent.Radio1Text)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.Radio2Text)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.Radio3Text)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.Radio1Url)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.Radio2Url)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.Radio3Url)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.ButtonText);
            }
        }

        public string Radio1Text
        {
            get
            {
                return _cmsPageComponent.Radio1Text;
            }
        }
        public string Radio2Text
        {
            get
            {
                return _cmsPageComponent.Radio2Text;
            }
        }
        public string Radio3Text
        {
            get
            {
                return _cmsPageComponent.Radio3Text;
            }
        }
        public string Radio1Url
        {
            get
            {
                return _cmsPageComponent.Radio1Url;
            }
        }
        public string Radio2Url
        {
            get
            {
                return _cmsPageComponent.Radio2Url;
            }
        }
        public string Radio3Url
        {
            get
            {
                return _cmsPageComponent.Radio3Url;
            }
        }
        public string ButtonText
        {
            get
            {
                return _cmsPageComponent.ButtonText;
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
