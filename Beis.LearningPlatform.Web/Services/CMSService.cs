using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Services
{
    public class CmsService : ICmsService
    {
        private readonly ILogger<CmsService> _logger;
        private readonly CmsOption _cmsOption;
        private readonly IMakeApiCallService _apiCallService;

        public CmsService(ILogger<CmsService> logger,
            IOptions<CmsOption> cmsOptions,
            IMakeApiCallService apiCallService) {
            _logger = logger;
            _cmsOption = cmsOptions.Value;
            _apiCallService = apiCallService;
        }

        public async Task<CMSPageViewModel> GetCustomPageResult(string strapiAction)
        {
            var result = await _apiCallService.GetApiResult(_cmsOption.ApiBaseUrl, strapiAction);
            var viewModel = string.IsNullOrWhiteSpace(result) ? new CMSPageViewModel() : JsonConvert.DeserializeObject<CMSPageViewModel>(result);
            //pagename is always expected from the front end so we can update this in case of empty return from web api
            if (string.IsNullOrWhiteSpace(viewModel.pagename))
            {
                viewModel.pagename = strapiAction;
            }
            return viewModel;
        }

        public async Task<IList<CMSSearchArticle>> GetSearchArticlesResult(string strapiAction)
        {
            var result = await _apiCallService.GetApiResult(_cmsOption.ApiBaseUrl, strapiAction);
            var viewModel = string.IsNullOrWhiteSpace(result) ? new List<CMSSearchArticle>() : JsonConvert.DeserializeObject<List<CMSSearchArticle>>(result);
            return viewModel;
        }

        public async Task<IList<CMSSearchTag>> GetSearchTagsResult(string strapiAction)
        {
            string result = await _apiCallService.GetApiResult(_cmsOption.ApiBaseUrl, strapiAction);
            var viewModel = string.IsNullOrWhiteSpace(result) ? new List<CMSSearchTag>() : JsonConvert.DeserializeObject<List<CMSSearchTag>>(result);
            return viewModel;
        }

        public async Task<CMSPageViewModel> FilterCustomPageResultByTags(IList<string> currentTagNames)
        {
            var viewModel = await GetCustomPageResult("Custom-pages/guidance-and-tools");
            var component = viewModel.components.Where(item => item.__component.Equals("page.reactive-tags", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (component == null)
            {
                return viewModel;
            }

            var articles = await GetSearchArticlesResult("search-articles");

            // We have a mix of links using displayName and name - and chaging the content is not easy - so try to handle both here:
            var allTags = articles.SelectMany(x => x.tags).ToList();
            var allTagNames = allTags
                .SelectMany(x => new string[] { x.name, x.displayName }).Distinct().ToList();
            var tagsNotInCms = currentTagNames.Except(allTagNames, StringComparer.OrdinalIgnoreCase).ToList();
            if (tagsNotInCms.Any())
            {
                _logger.LogWarning($"Tags not recognised {string.Join(",", tagsNotInCms)}");
                return viewModel;
            }

            // Again currentTagNames can be either name or displayName - so get actual tags here:
            component.SelectedTags = allTags.Where(x => currentTagNames.Intersect(new string[] { x.name, x.displayName }).Any())
                .Select(x => x.displayName).Distinct().ToList(); 

            var relatedArticles = articles
                .Where(article => // Again either name or displayName can match
                article.tags.SelectMany(x => new string[] { x.name, x.displayName }).Intersect(currentTagNames, StringComparer.OrdinalIgnoreCase).Any())  
                .ToList();
            relatedArticles.Sort();
            component.search_articles = relatedArticles;
            return viewModel;
        }       

        public async Task<IList<ComparisonToolPageViewModel>> GetComparisonToolPageResult(string strapiAction)
        {
            var result = await _apiCallService.GetApiResult(_cmsOption.ApiBaseUrl, strapiAction);
            var viewModel  = string.IsNullOrWhiteSpace(result) ? new List<ComparisonToolPageViewModel>() : JsonConvert.DeserializeObject<List<ComparisonToolPageViewModel>>(result);
            return viewModel;
        }

        public async Task<IEnumerable<SiteNavigationModel>> GetSiteNavigation()
        {
            var result = await _apiCallService.GetApiResult(_cmsOption.ApiBaseUrl, "site-navigations");
            var viewModel = string.IsNullOrWhiteSpace(result) ? new List<SiteNavigationModel>() : JsonConvert.DeserializeObject<List<SiteNavigationModel>>(result);
            return viewModel;
        }

    }
}