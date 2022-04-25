using Beis.LearningPlatform.Web.StrapiApi.Models;
using System;

namespace Beis.LearningPlatform.Web.Models
{
    public class CmsStatisticsTextViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsStatisticsTextViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get {
                return !string.IsNullOrWhiteSpace(_cmsPageComponent.text) 
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.statisticText)
                    && !string.IsNullOrWhiteSpace(_cmsPageComponent.statisticNumber)
                    && _cmsPageComponent.statisticBoxAlignment.HasValue;
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

        public string HtmlText { get; set; }
        public string HtmlStatisticText { get; set; }
        public string ClassNameBackgroundColour { get; set; }
        public string ClassNameStatisticNumberColour { get; internal set; }
        public string ClassNameStatisticTextColor { get; internal set; }

    }
}