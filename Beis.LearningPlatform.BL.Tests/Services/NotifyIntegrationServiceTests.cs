using Beis.LearningPlatform.BL.IntegrationServices;
using Beis.LearningPlatform.BL.IntegrationServices.GovUkNotify;
using Beis.LearningPlatform.BL.IntegrationServices.Options;
using Beis.LearningPlatform.Data.Entities.Skills;
using Microsoft.Extensions.Options;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class NotifyIntegrationServiceTests
    {
        private INotifyIntegrationService _notifyService;
        private Mock<ILogger<NotifyIntegrationService>> _logger;
        private Mock<INotifyService> _notifyServiceMock;
        private IOptions<NotifyServiceOption> _notifyServiceOpt;
        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<NotifyIntegrationService>>();
            _notifyServiceMock = new Mock<INotifyService>();
            _notifyServiceOpt = Options.Create(new NotifyServiceOption { Templates = new Templates { DtResultPageQ6Yes = Guid.NewGuid().ToString(), DtResultPageQ6No = Guid.NewGuid().ToString() } });
            _notifyService = new NotifyIntegrationService(_logger.Object, _notifyServiceOpt, _notifyServiceMock.Object);
           
        }


        [Test]
        public void SendTemplateEmail_ValidData_Success()
        {

            string emailAddress = "test@test.com";
            string templateId = "test Template";
            var personalisation = new Dictionary<string, dynamic>()
            {{"UK", "London"},
                {"USA", "Chicago"},
                {"India", "Mumbai"}};
            _notifyService.SendDiagnosticToolResult(emailAddress, templateId, personalisation);
        }

        [Test]
        public void SendTemplateEmail_NullData_Success()
        {

            string emailAddress = "test";
            string templateId = "test Template";
            var personalisation = new Dictionary<string, dynamic>()
            {{"UK", "London"},
                {"USA", "Chicago"},
                {"India", "Mumbai"}};
            
            _notifyService.SendDiagnosticToolResult(null, null, null);
        }
    }
}
