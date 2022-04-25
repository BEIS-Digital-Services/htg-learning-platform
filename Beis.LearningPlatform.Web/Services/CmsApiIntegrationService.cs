using Beis.LearningPlatform.Web.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Services
{
    /// <summary>
    /// A class that implements a service that integrates with a CMS's API.
    /// </summary>
    public class CmsApiIntegrationService : ICmsApiIntegrationService
    {
        static CmsApiIntegrationService()
        {
            _httpClient = new();
        }

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="cmsOptions">An IOptions&lt;CmsOption&gt; that is the CMS configuration to use.</param>
        public CmsApiIntegrationService(ILogger<CmsApiIntegrationService> logger,
                                        IOptions<CmsOption> cmsOptions)
        {
            _logger = logger;
            _cmsOption = cmsOptions.Value;
        }

        private readonly CmsOption _cmsOption;
        private static HttpClient _httpClient;
        private readonly ILogger _logger;

        private string BuildUrl(string apiAction)
        {
            if (!_cmsOption.ApiBaseUrl.EndsWith('/'))
                apiAction = $"/{apiAction}";

            return $"{_cmsOption.ApiBaseUrl}{apiAction}";
        }

        private HttpClient CreateHttpClient()
        {
            if (_httpClient == default)
            {
                _httpClient = new();
                _httpClient.BaseAddress = new Uri(_cmsOption.ApiBaseUrl);
                _httpClient.DefaultRequestHeaders.Clear();
            }

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _httpClient;
        }

        async Task<string> ICmsApiIntegrationService.Get(string apiAction)
        {
            var httpClient = CreateHttpClient();
            string returnValue = default;
            var url = BuildUrl(apiAction);

            try
            {
                // Get data from API
                var result = await httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                    returnValue = await result.Content.ReadAsStringAsync();
                else
                    _logger.LogWarning($"CMS returned response {result.StatusCode} from call to {url}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error whilst GET to {url}");
                throw new InvalidOperationException("Unable to GET result from CMS API", ex);
            }

            return returnValue;
        }
    }
}