using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Utils;
using System;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageViewModel : IPageViewModel
    {
        public bool ShowBackButton { get; internal set; }

        public int id { get; set; }
        public DateTime? published_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string pageTitle { get; set; }
        public string pagename { get; set; }        
        public IList<CMSPageNavigation> navigations { get; set; }
        public IList<CMSPageComponent> components { get; set; }

        public IList<CMSPageHeroBanner> hero_banners { get; set; }
        public IList<CMSPageFooter> footers { get; set; }
        public IList<CMSPageSideNavigation> side_navigations { get; set; }

        public string CmsBaseUrl { get; set; }

        public bool ShowLayoutNavigation => this.ShowLayoutNavigation();
        public PartialNavigationModel NavigationModel => this.GetPartialNavigationModel();

        public string pagenameAsTitle
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.pagename))
                {
                    return this.pagename;
                }

                return this.pagename.Replace("-", " ");
            }
        }
        
        public string ContentKey => $"cms-page-{pagename}";

        // SEO properties:
        public string title { get; set; }
        public string description { get; set; }
        public string meta { get; set; }
        public bool? index { get; set; } = true; // Default to index
        public bool? follow { get; set; } = true; // Default to follow
		public bool? PreviewSearchArticles { get; internal set; }
	}

}
