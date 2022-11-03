using Beis.LearningPlatform.BL.IntegrationServices;
using Beis.LearningPlatform.Data.Entities.Skills;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class SkillsTwoServiceTests
    {
        private ISkillsTwoService _service;
        private Mock<ISkillsTwoDataService> _dataService;

        [SetUp]
        public void Setup()
        {
            var logger = new Mock<ILogger<EmailService>>();
            var notifyService = new Mock<INotifyIntegrationService>();
            _dataService = new Mock<ISkillsTwoDataService>();

            _service = new SkillsTwoService(
                logger.Object,
                notifyService.Object,
                _dataService.Object
            );
        }
        
        [Test]
        public void SaveSkillsTwoResponse_NullData_ThrowsException()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() =>
                _service.SaveSkillsTwoResponse(Guid.NewGuid(), null)
            );
            
            Assert.That(ex.ParamName, Is.EqualTo("skillsTwoResponse"));
        }

        [Test]
        public async Task SaveSkillsTwoResponse_ValidData_Success()
        {
            var responseInt = 123;
            
            _dataService
                .Setup(d => d.Add(It.IsAny<SkillsTwoResponse>()))
                .ReturnsAsync(responseInt);

            var requestId = Guid.NewGuid();
            var data = new SkillsTwoResponse();

            var result = await _service.SaveSkillsTwoResponse(requestId, data);
            
            Assert.That(result.RequestID, Is.EqualTo(requestId));
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Message, Is.Null);
            Assert.That(result.Payload, Is.EqualTo(responseInt));
        }
    }
}