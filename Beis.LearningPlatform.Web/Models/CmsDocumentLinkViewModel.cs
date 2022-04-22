using Beis.LearningPlatform.Web.StrapiApi.Models;

namespace Beis.LearningPlatform.Web.Models
{
    public class CmsDocumentLinkViewModel
	{
		public string TargetUrl { get; set; }
		public string ImageUrl { get; set; }
		public CMSPageComponent Component { get; set; }

		public string CssClassTopSpace
		{
			get
			{
				return Component.topSpace == "nospace" ? string.Empty : " govuk-!-margin-top-6";
			}
		}

		public string CssClassBottomSpace
		{
			get
			{
				return Component.bottomSpace == "nospace" ? string.Empty : " govuk-!-margin-bottom-6";
			}
		}

	}
}