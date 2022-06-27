namespace Beis.LearningPlatform.Web.Models
{
    public class ErrorViewModel : IPageViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrWhiteSpace(RequestId);

        public string ErrorMessage { get; set; }

        public string SessionId { get; set; }

        #region IPageViewModel

        public string pageTitle { get; set; } = "Help to Grow: Digital - Error";
        public string pagename { get; set; } = "Error Page";
        public IList<CMSPageNavigation> navigations => null;
        public IList<CMSPageFooter> footers => null;
        public IList<CMSPageSideNavigation> side_navigations => null;
        public string CmsBaseUrl { get; set; }
        public bool ShowLayoutNavigation => this.ShowLayoutNavigation();
        public PartialNavigationModel NavigationModel => this.GetPartialNavigationModel();

        public string ContentKey => "error-page";

        // SEO properties:
        public string title { get; set; } = "Help to Grow: Digital - Error";
        public string description { get; set; } = "Application Error Occured.";
        public string meta { get; }
        public bool? index { get; } = false;
        public bool? follow { get; } = true;
		public int id { get; }

		#endregion
	}
}