using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class CmsControllerHelperBase : ControllerHelperBase, ICmsControllerHelperBase
    {
        private readonly CmsOption _cmsOption;
        private readonly ICmsService _cmsService;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ICmsFeedbackService _cmsFeedbackService;

        protected CmsControllerHelperBase(
            ILogger<CmsControllerHelperBase> logger,
            IOptions<CmsOption> cmsOptions,
            ICmsService cmsService,
            ICmsFeedbackService cmsFeedbackService,
            IHttpContextAccessor httpContextAccessor)
            : base(logger)
        {
            _cmsOption = cmsOptions.Value;
            _cmsService = cmsService;
            _cmsFeedbackService = cmsFeedbackService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CMSPageViewModel> ProcessGetCustomPageResult(string strapiAction)
        {
            var viewModel = await _cmsService.GetCustomPageResult(strapiAction);
            viewModel.CmsBaseUrl = _cmsOption.ApiBaseUrl;
            return viewModel;
        }


        public async Task<IList<CMSSearchArticle>> ProcessGetSearchArticlesResult(string strapiAction)
        {
            var viewModel = await _cmsService.GetSearchArticlesResult(strapiAction);
            return viewModel;
        }
        
        public async Task<CMSPageViewModel> ProcessFilterCustomPageResultByTags(IList<string> currentTagNames)
        {
            var viewModel = await _cmsService.FilterCustomPageResultByTags(currentTagNames);
            viewModel.CmsBaseUrl = _cmsOption.ApiBaseUrl;
            return viewModel;
        }

        public async Task<CMSPageViewModel> ProcessFilterCustomPageResultByTags(IList<string> currentTagNames, string cmsDisplayName)
        {
            var viewModel = await _cmsService.FilterCustomPageResultByTags(currentTagNames);
            viewModel.CmsBaseUrl = _cmsOption.ApiBaseUrl;
            return viewModel;
        }

        public void SetBackButton(IController controller)
        {
            controller.ViewData["showBackButton"] = true;
        }
    }
}