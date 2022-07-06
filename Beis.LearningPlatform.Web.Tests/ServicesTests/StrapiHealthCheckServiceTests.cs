using Microsoft.Extensions.Diagnostics.HealthChecks;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ServicesTests
{
    public class StrapiHealthCheckServiceTests
    {
        
        private readonly IOptions<CmsOption> _cmsOptions = ConfigOptions.Create(new CmsOption());
        private StrapiMakeApiCallMockService _MakeApiCallService;
        
        private Mock<ILogger<StrapiHealthCheckService>> _logger;
        private Mock<IMakeApiCallService> _makeApiCallService;
        private Mock<IHealthCheck> _healthCheck;
        private StrapiHealthCheckService _strapiHealthCheckService;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<StrapiHealthCheckService>>();
            _healthCheck = new Mock<IHealthCheck>();
            _makeApiCallService = new Mock<IMakeApiCallService>();
            _strapiHealthCheckService = new StrapiHealthCheckService(_logger.Object, _cmsOptions, _makeApiCallService.Object);
        }

        [Test]
        public async Task CheckHealth_When_data_is_empty_Returns_Unhealthy()
        {

            _makeApiCallService.Setup(m => m.GetApiResult(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(string.Empty);
            var healthContext = new HealthCheckContext() {
                Registration = new HealthCheckRegistration("healthCheck", _healthCheck.Object, null, null)
            };
            var result = await _strapiHealthCheckService.CheckHealthAsync(healthContext, CancellationToken.None) ;
            
            result.Status.Should().Be(HealthStatus.Unhealthy);

        }

        [Test]
        public async Task CheckHealth_When_service_throws_exception_Returns_Unhealthy()
        {

            _makeApiCallService.Setup(m => m.GetApiResult(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Failed"));
            var healthContext = new HealthCheckContext()
            {
                Registration = new HealthCheckRegistration("healthCheck", _healthCheck.Object, null, null)
            };
            var result = await _strapiHealthCheckService.CheckHealthAsync(healthContext, CancellationToken.None);

            result.Status.Should().Be(HealthStatus.Unhealthy);

        }

        [Test]
        public async Task CheckHealth_When_data_is_not_empty_Returns_Healthy()
        {

            _makeApiCallService.Setup(m => m.GetApiResult(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("some data");
            var healthContext = new HealthCheckContext()
            {
                Registration = new HealthCheckRegistration("healthCheck", _healthCheck.Object, null, null)
            };
            var result = await _strapiHealthCheckService.CheckHealthAsync(healthContext, CancellationToken.None);

            result.Status.Should().Be(HealthStatus.Healthy);

        }
    }
}
