namespace Beis.LearningPlatform.Web.Utils
{
    public static class CmsPageLinkExtensions
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

        internal static string GetCmsLinkUrl(this CMSPageLink cmsPageLink)
        {
            return cmsPageLink.url.StartsWith("/") ? cmsPageLink.url.Substring(1) : cmsPageLink.url.Substring(0, cmsPageLink.url.IndexOf("/", StringComparison.Ordinal) - 1);
        }

        internal static string GetCustomClass(this CMSPageLink cmsPageLink)
        {
            return CamelCaseConverter.Delimiter(cmsPageLink.custom_class, "-");
        }
	}
}