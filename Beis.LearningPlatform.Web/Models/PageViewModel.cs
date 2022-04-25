using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.Utils;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Models
{
    public class PageViewModel : IPageViewModel
    {
        public string pageTitle { get; set; }
        public string pagename { get; set; }

        public IList<CMSPageNavigation> navigations => null;
        public IList<CMSPageFooter> footers => null;
        public IList<CMSPageSideNavigation> side_navigations => null;
        public string CmsBaseUrl { get; set; }
        public bool ShowLayoutNavigation => this.ShowLayoutNavigation();
        public PartialNavigationModel NavigationModel => this.GetPartialNavigationModel();

        public string ContentKey => $"page-view-{pagename}";

        // SEO properties:
        public string title { get; }
        public string description { get; }
        public string meta { get; }
        public bool? index { get; } = true;
        public bool? follow { get; } = true;
    }
}