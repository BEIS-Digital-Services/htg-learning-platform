using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Beis.LearningPlatform.Web.Services
{
    public class StrapiHealthCheckService : IHealthCheck
    {
        private readonly ILogger<StrapiHealthCheckService> _logger;
        private readonly CmsOption _cmsOption;
        private readonly IMakeApiCallService _apiCallService;
        public StrapiHealthCheckService(ILogger<StrapiHealthCheckService> logger,
            IOptions<CmsOption> cmsOptions,
            IMakeApiCallService apiCallService)
        {
            _logger = logger;
            _cmsOption = cmsOptions.Value;
            _apiCallService = apiCallService;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;

            try
            {
                var result = await _apiCallService.GetApiResult(_cmsOption.ApiBaseUrl, "Custom-pages/cookies");

                if (string.IsNullOrWhiteSpace(result))
                {
                    isHealthy = false;
                    _logger.LogError("Strapi Healthcheck failed.");
                }
            }
            catch (Exception e)
            {
                isHealthy = false;
            }

            if (isHealthy)
            {
                return await Task.FromResult(
                    HealthCheckResult.Healthy("Strapi Healthcheck passed."));
            }

            return await Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, "Strapi Healthcheck failed."));
        }

    }
}
