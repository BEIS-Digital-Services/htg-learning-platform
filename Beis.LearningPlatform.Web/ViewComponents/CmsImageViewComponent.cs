using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsImageViewComponent : ViewComponent
    {
        private readonly string _cmsBaseUrl;

        public CmsImageViewComponent(IOptions<CmsOption> cmsOptions)
        {
            _cmsBaseUrl = cmsOptions.Value.ApiBaseUrl;
        }


        /// <remarks>
        /// We are passing in a CmsImageViewModel because aspnet mvc currently does not support optional parameters (or overloaded ctors) on ViewComponents.
        /// </remarks>
        public IViewComponentResult Invoke(CmsImageViewModel options)
        {
            options.BaseUrl = _cmsBaseUrl;
            return View(options);
        }
    }
}