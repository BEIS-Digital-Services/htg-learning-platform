namespace Beis.LearningPlatform.Web.Utils
{
    public static class CmsPageLinkExtensions
	{
		public static string GetGaLinkId(this ICmsPageLink cMSPageLink, string prefix = null)
		{
			if (string.IsNullOrWhiteSpace(cMSPageLink?.label) && string.IsNullOrWhiteSpace(cMSPageLink?.url))
			{
				return $"{prefix}{cMSPageLink?.id}";
			}

			return $"{prefix?.Trim()}{cMSPageLink?.label?.Trim()}{cMSPageLink?.url?.Trim()}".Replace("/", "-").Replace(" ", "-").UrlEncode(true);
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