using Beis.LearningPlatform.BL.IntegrationServices;
using Beis.LearningPlatform.BL.IntegrationServices.GovUkNotify;
using Beis.LearningPlatform.Data.Entities.Skills;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class NotifyServiceTests
    {
        private INotifyService _notifyService;
        private Mock<ILogger<NotifyService>> _logger;
        private Mock<INotifyService> _notifyServiceMock;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<NotifyService>>();
            _notifyServiceMock = new Mock<INotifyService>();
            _notifyService = new NotifyService(_logger.Object);
        }

        [Test]
        public void ConfigureService_ValidData_Success()
        {
            _notifyService.ConfigureService("url", "key");
        }

        [Test]
        public void SendTemplateEmail_ValidData_Success()
        {
            string emailAddress = "test@email.com";
            string templateId = "test Template";
            var personalisation = new Dictionary<string, dynamic>()
                {{"UK", "London"},
                    {"USA", "Chicago"},
                    {"India", "Mumbai"}};
            _notifyService.SendTemplateEmail(emailAddress, templateId,personalisation);
        }

        [Test]
        public void SendTemplateEmail_NullData_Success()
        {
            string emailAddress = string.Empty;
            string templateId = "test Template";
            var personalisation = new Dictionary<string, dynamic>()
            {{"UK", "London"},
                {"USA", "Chicago"},
                {"India", "Mumbai"}};
            _notifyService.SendTemplateEmail(emailAddress, templateId, personalisation);
        }

        [Test]
        public void SendTemplateEmailList_ValidData_Success()
        {
            string[] emailAddress = {"test@email.com","Test1.test.com"};
            string templateId = "test Template";
            var personalisation = new Dictionary<string, dynamic>()
            {{"UK", "London"},
                {"USA", "Chicago"},
                {"India", "Mumbai"}};
            
            _notifyService.SendTemplateEmail(emailAddress, templateId, personalisation);
        }

        [Test]
        public void ConfigureService_ValidData_Error()
        {
            _notifyService.ConfigureService("url", "key");
            var ex = Assert.Throws<InvalidOperationException>(() => _notifyService.ConfigureService("url", "key"));
            Assert.That(ex.Message == "The Notify Service has already been configured");
        }
    }
}
