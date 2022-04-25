using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Services
{
    public class CmsService2 : ICmsService2
    {
        public CmsService2(ILogger<CmsService2> logger,
                           ICmsApiIntegrationService cmsApiIntegrationService,
                           IOptions<CmsOption> cmsOptions)
        {
            _logger = logger;
            _cmsApiIntegrationService = cmsApiIntegrationService;
            _cmsOption = cmsOptions.Value;
            _serviceInterface = this;
        }

        private readonly ICmsApiIntegrationService _cmsApiIntegrationService;
        private readonly CmsOption _cmsOption;
        private readonly ILogger _logger;
        private readonly ICmsService2 _serviceInterface;

        async Task<(bool IsSuccessful, T Page)> GetPage<T>(string pageReference)
        {
            T page = default;
            string result;
            bool returnValue = false;

            _logger.LogInformation($"CMS Service Get Page \"{pageReference}\"");

            result = await _cmsApiIntegrationService.Get(pageReference);
            if (!string.IsNullOrWhiteSpace(result))
            {
                page = JsonSerializer.Deserialize<T>(result);
                returnValue = true;
            }

            return (returnValue, page);
        }

        async Task<CMSPageViewModel> ICmsService2.GetPage(string pageReference)
        {
            CMSPageViewModel returnValue = default;

            var (IsSuccessful, Page) = await GetPage<CMSPageViewModel>(pageReference);
            if (IsSuccessful)
            {
                returnValue = Page;
                returnValue.pagename = pageReference;
                returnValue.CmsBaseUrl = _cmsOption.ApiBaseUrl;
            }

            return returnValue;
        }

        async Task<CMSSearchArticle[]> ICmsService2.GetSearchArticles()
        {
            var (_, Page) = await GetPage<CMSSearchArticle[]>("search-articles");
            return Page;
        }

        async Task<CMSSearchArticle[]> ICmsService2.GetSearchArticles(IEnumerable<string> searchTags)
        {
            var articles = await _serviceInterface.GetSearchArticles();
            var returnValue = articles.Where(a => a.tags.Any(t => searchTags.Any(st => st.Equals(t.name, StringComparison.OrdinalIgnoreCase))));
            return returnValue.ToArray();
        }
    }
}