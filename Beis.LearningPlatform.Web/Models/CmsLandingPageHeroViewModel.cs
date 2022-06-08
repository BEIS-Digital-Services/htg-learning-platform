namespace Beis.LearningPlatform.Web.Models
{
    public class CmsLandingPageHeroViewModel
	{
		public string Header { get; set; }
		public string Intro { get; set; }
		public CMSPageImage Image { get; set; }
		public string LinkText { get; set; }
		public string LinkUrl { get; set; }
		public string LinkAria { get; set; }

		public bool AlternateLayout { get; set; }

		public bool HasContent
        {
            get
            {
				return !string.IsNullOrEmpty(Image?.url)
					&& !string.IsNullOrEmpty(Header)
					&& !string.IsNullOrEmpty(Intro);
            }
        }

        public bool HasLink
        {
            get
            {
				return !string.IsNullOrEmpty(LinkUrl)
					&& !string.IsNullOrEmpty(LinkText);
            }
        }

	}
}