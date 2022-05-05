using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Interfaces
{
    public interface IPageViewModel
    {
        public int id { get; }
        string pageTitle { get; set; }
        string pagename { get; set; }
        IList<CMSPageNavigation> navigations { get; }
        IList<CMSPageFooter> footers { get; }
        IList<CMSPageSideNavigation> side_navigations { get; }
        string CmsBaseUrl { get; set; }
        bool ShowLayoutNavigation { get; }
        PartialNavigationModel NavigationModel { get; }

        // SEO properties:
        public string title { get; }
        public string description { get; }
        public string meta { get; }
        public bool? index { get; }
        public bool? follow { get; }

        /// <summary>
        /// Used for GA DataLayer. This should uniquely identify the content of the page view 
        /// - also in circumstances where the same URL might return different content/options.
        /// </summary>
        public string ContentKey { get; }
    }
}