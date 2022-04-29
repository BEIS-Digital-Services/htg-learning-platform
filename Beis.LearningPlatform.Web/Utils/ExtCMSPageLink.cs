using Beis.LearningPlatform.Web.StrapiApi.Models;

namespace Beis.LearningPlatform.Web.Utils
{
	public static class ExtCMSPageLink
	{
		public static string GetGaLinkId(this ICmsPageLink cMSPageLink)
		{
			if (string.IsNullOrWhiteSpace(cMSPageLink?.label))
			{
				return $"{cMSPageLink?.id}";
			}

			var labelId = cMSPageLink?.label.Replace(" ", "-").UrlEncode(true);
			return $"{labelId}-{cMSPageLink?.id}";
		}

	}
}
