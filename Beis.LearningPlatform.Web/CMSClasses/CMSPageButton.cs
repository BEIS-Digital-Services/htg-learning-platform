using Beis.LearningPlatform.Web.Utils;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageButton
    {
        public int id { get; set; }
        public string label { get; set; }
        public string url { get; set; }
        public string anchor { get; set; }
        public string type { get; set; }
        public string anchorlink { get; set; }
        public string custom_class { get; set; }
        public CMSPageIcon icon { get; set; }

		public string GetGaLinkId()
		{
			if (string.IsNullOrWhiteSpace(label))
			{
				return $"{id}";
			}

			var labelId = label.Replace(" ", "-").UrlEncode(true);
			return $"{labelId}-{id}";
		}
	}
}