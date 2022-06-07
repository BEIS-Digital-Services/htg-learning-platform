namespace Beis.LearningPlatform.Web.Models
{
    public class CookiePageViewModel : IPageViewModel
    {
        public UserCookiePreferencesModel UserCookiePreferences { get; set; }
        public bool CookieProcessed { get; set; }

        public string pageTitle { get; set; } = "Help to Grow: Digital - Cookie Preferences";
        public string pagename { get; set; } = "Cookies";

        public IList<CMSPageNavigation> navigations => null;
        public IList<CMSPageFooter> footers => null;
        public IList<CMSPageSideNavigation> side_navigations => null;
        public string CmsBaseUrl { get; set; }
        public bool ShowLayoutNavigation => this.ShowLayoutNavigation();
        public PartialNavigationModel NavigationModel => this.GetPartialNavigationModel();

        // SEO properties:
        public string title { get; } = "Cookies on Help to Grow: Digital and how we use these files";
        public string description { get; } = "Cookies are files saved on your phone, tablet or computer when you visit a website. We use these cookies to store information about how you use our website.";
        public string meta { get; } = null;
        public bool? index { get; } = true;
        public bool? follow { get; } = true;
        public int id { get; }

        public string ContentKey => "cookie-page";
	}
}