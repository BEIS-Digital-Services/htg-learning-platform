namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
	public class CMSSimpleLink : ICmsPageLink
	{
		public int id { get; set; }
		public string LinkText { get; set; }
		public string LinkUrl { get; set; }

		public bool HasContent
		{
			get
			{
				return !string.IsNullOrEmpty(LinkText)
					&& !string.IsNullOrEmpty(LinkUrl);
			}
		}

        string ICmsPageLink.label { get { return LinkText; } }
    }
}