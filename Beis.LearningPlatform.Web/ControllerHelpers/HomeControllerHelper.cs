using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class HomeControllerHelper : CmsControllerHelperBase, IHomeControllerHelper
    {
        public HomeControllerHelper(ILogger<HomeControllerHelper> logger,
                ICmsService cmsService,
                ICmsFeedbackService cmsFeedbackService,
                IOptions<CmsOption> cmsOptions,
                IHttpContextAccessor httpContextAccessor
            ) : base(logger, cmsOptions, cmsService, cmsFeedbackService, httpContextAccessor) {}

        public async Task SetReactiveTagComponents(CMSPageViewModel viewModel)
        {
            var component = viewModel.components.Where(item => item.__component.Equals("page.reactive-tags", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (component != null)
            {
                component.search_articles = await ProcessGetSearchArticlesResult("search-articles");
                component.search_articles.OrderBy(x => x.order);
            }
        }

        public IList<string> GetCurrentTags(string yourTags)
        {
            return (string.IsNullOrWhiteSpace(yourTags) ? Array.Empty<string>() : yourTags.Split(','))
                .Distinct()
                .ToList();
        }

        public IList<string> GetCurrentTags(string yourTags, string tag, bool removeTag)
        {
            var currentTags = string.IsNullOrWhiteSpace(yourTags) ? new() : yourTags.Split(',').Distinct().ToList();
            if (removeTag)
            {
                currentTags.Remove(tag);
            }
            else
            {
                currentTags.Add(tag);
            }

            return currentTags.Distinct().ToList(); // Page reloads on adding URLs -> distinct .. 
        }
    }
}