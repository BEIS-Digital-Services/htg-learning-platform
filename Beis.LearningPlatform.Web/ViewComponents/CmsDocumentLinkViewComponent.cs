namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsDocumentLinkViewComponent : ViewComponent
    {
        private readonly string _cmsBaseUrl;

        public CmsDocumentLinkViewComponent(IOptions<CmsOption> cmsOptions)
        {
            _cmsBaseUrl = cmsOptions.Value.ApiBaseUrl;
        }


        /// <remarks>
        /// We are passing in a CmsImageViewModel because aspnet mvc currently does not support optional parameters (or overloaded ctors) on ViewComponents.
        /// </remarks>
        public IViewComponentResult Invoke(CMSPageComponent component)
        {
            var targetURL = GetTargetUrl(component);
            var imageUrl = GetImageUrl(component);

            var viewModel = new CmsDocumentLinkViewModel
            {
                TargetUrl = $"{_cmsBaseUrl}{targetURL}",
                ImageUrl = $"{_cmsBaseUrl}{imageUrl}",
                Component = component
            };
            return View(viewModel);
        }

        /// <summary>
        /// Copied from existing view.
        /// </summary>
        private static string GetImageUrl(CMSPageComponent component)
        {
            if (component.media == null || component.media.Count < 1)
            {
                return null;
            }

            return component.media[0].mime == "application/pdf" ? "/assets/images/pdf.svg" : component.media[0].url;
        }

        /// <summary>
        /// Logic here a bit unclear- copied from existing view.
        /// </summary>
        private static string GetTargetUrl(CMSPageComponent component)
        {
            var targetUrl = component.url;

            if (component.media?.Count > 0)
            {
                var media = component.media[0];

                if (string.IsNullOrWhiteSpace(targetUrl))
                    targetUrl = media.url;
            }

            return targetUrl;
        }
    }
}