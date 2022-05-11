using Beis.LearningPlatform.Web.StrapiApi.Models;

namespace Beis.LearningPlatform.Web.Utils
{
	public static class ExtCMSPageLink
	{
		public static string GetGaLinkId(this ICmsPageLink cMSPageLink, string prefix = null)
		{
			if (string.IsNullOrWhiteSpace(cMSPageLink?.label))
			{
				return $"{prefix}{cMSPageLink?.id}";
			}

			var labelId = $"{prefix}{cMSPageLink?.label}".Trim().Replace(" ", "-").UrlEncode(true);
			return $"{labelId}-{cMSPageLink?.id}";
		}

	}
}
