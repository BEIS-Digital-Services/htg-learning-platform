namespace Beis.LearningPlatform.Web.Models
{
    public class CmsAdaptiveImageViewModel
	{
		private readonly CMSPageComponent _cmsPageComponent;
		private readonly string _baseUrl;
		private readonly bool _hasDesktopImage;
		private readonly bool _hasMobileImage;
		private readonly bool _hasContent;

		public CmsAdaptiveImageViewModel(CMSPageComponent cmsPageComponent, string baseUrl)
		{
			_cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
			if (string.IsNullOrEmpty(baseUrl))
			{
				throw new ArgumentException("Base url required", nameof(baseUrl));
			}
			_baseUrl = baseUrl;
			_hasDesktopImage = cmsPageComponent.desktop_image != null && cmsPageComponent.desktop_image.HasContent;
			_hasMobileImage = cmsPageComponent.mobile_image != null && cmsPageComponent.mobile_image.HasContent;
			_hasContent = _hasDesktopImage && _hasMobileImage;
		}

		public bool HasContent
		{
			get
			{
				return _hasContent;
			}
		}

		public CMSPageComponent Component
		{
			get
			{
				return _cmsPageComponent;
			}
		}

		public string BaseUrl
		{
			get
			{
				return this._baseUrl;
			}
		}

		public string DesktopImageUrl
		{
			get
			{
				if (!_hasDesktopImage)
				{
					return null;
				}
				return $"{_baseUrl}{_cmsPageComponent.desktop_image.url}";;
			}
		}

		public string MobileImageUrl
		{
			get
			{
				if (!_hasMobileImage)
				{
					return null;
				}
				return $"{_baseUrl}{_cmsPageComponent.mobile_image.url}";;
			}
		}

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