namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
	public class CMSColumnHero : ICmsPageLink
	{
		public int id { get; set; }
		public string Header { get; set; }
		public string Intro { get; set; }
		public string LinkText { get; set; }
		public string LinkUrl { get; set; }

		public bool HasContent
		{
			get
			{
				return !string.IsNullOrEmpty(Header)
					&& !string.IsNullOrEmpty(Intro)
					&& !string.IsNullOrEmpty(LinkText)
					&& !string.IsNullOrEmpty(LinkUrl);
			}
		}

        string ICmsPageLink.label { get { return LinkText; } }
        string ICmsPageLink.url { get { return LinkUrl; } }
    }
}