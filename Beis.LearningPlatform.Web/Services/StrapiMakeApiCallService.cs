namespace Beis.LearningPlatform.Web.Services
{
    public class StrapiMakeApiCallService : IMakeApiCallService
    {
        private readonly ILogger<StrapiMakeApiCallService> _logger;

        public StrapiMakeApiCallService(ILogger<StrapiMakeApiCallService> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetApiResult(string baseUrl, string strapiAction)
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await httpClient.GetAsync(strapiAction);

            if (!result.IsSuccessStatusCode)
            {
                _logger.LogWarning("Request {strapiAction} returned {result.StatusCode}", strapiAction, result.StatusCode);
                return string.Empty;
            }
            return await result.Content.ReadAsStringAsync();
        }
    }
}