using Beis.LearningPlatform.Web.StrapiApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beis.LearningPlatform.Web.Models
{
    public class CmsAccordionListViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsAccordionListViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent;
        }

        public bool HasContent
        {
            get
            {
                return AccordionItems.Any(x => x.HasContent);
            }
        }

        public CMSPageComponent Component
        {
            get
            {
                return _cmsPageComponent;
            }
        }

        public List<CmsAccordionItemViewModel> AccordionItems
        {
            get
            {
                return _cmsPageComponent.AccordionItems;
            }
        }
    }
}