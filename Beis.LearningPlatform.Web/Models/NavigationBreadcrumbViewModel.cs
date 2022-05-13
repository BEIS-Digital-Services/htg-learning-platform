using Beis.LearningPlatform.Web.StrapiApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beis.LearningPlatform.Web.Models
{
    public class NavigationBreadcrumbViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public NavigationBreadcrumbViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get
            {
                return SiteNavigationModel != null
                    && SiteNavigationModel.SiteNavigationModels?.Any() == true;
            }
        }

        private IList<CMSSimpleLink> _breadcrumbs;
        public IList<CMSSimpleLink> Breadcrumbs
        {
            get
            {
                if (_breadcrumbs == null)
                { 
                    _breadcrumbs = GetBreadcrumbs();
                }
                return _breadcrumbs;
            }
        }

        private IList<CMSSimpleLink> GetBreadcrumbs()
        {
            var rtnList = new List<CMSSimpleLink>();
            if (!HasContent)
            {
                return new List<CMSSimpleLink>();
            }


            var topNavLink = SiteNavigationModel
                .SiteNavigationModels
                .Where(x => x.NavigationItem?.IsActive == true)
                .FirstOrDefault();

            if (topNavLink?.NavigationItem != null)
            {
                rtnList.Add(new CMSSimpleLink { LinkText = topNavLink.NavigationItem.Title, LinkUrl = topNavLink.NavigationItem.GetMenuUrl() });

                var subNavLink = topNavLink.SubNavigationItems?
                .Where(x => x?.IsActive == true)
                .Select(x => new CMSSimpleLink { LinkText = x.Title, LinkUrl = x.GetMenuUrl() })
                .FirstOrDefault();

                if (subNavLink != null)
                {
                    rtnList.Add(subNavLink);
                }
            }

            if (_cmsPageComponent.AdditionalLinks?.Any() == true)
            {
                rtnList.AddRange(_cmsPageComponent.AdditionalLinks);
            }

            return rtnList;
        }

        public IList<CMSSimpleLink> AdditionalLinks
        {
            get
            {
                return _cmsPageComponent.AdditionalLinks;
            }
        }

        public SiteNavigationViewModel SiteNavigationModel { get; set; }

        public CMSPageComponent Component
        {
            get
            {
                return this._cmsPageComponent;
            }
        }
    }
}