using Beis.LearningPlatform.BL.IntegrationServices;
using Beis.LearningPlatform.Data.Entities.Skills;
using System;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class SkillsTwoServiceTests
    {
        private ISkillsTwoService _skills2Service;
        private Mock<ILogger<EmailService>> _logger;
        private Mock<INotifyIntegrationService> _notifyIntegrationService;
        private Mock<ISkillsTwoDataService> _skills2DataServiceMock;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<EmailService>>();
            _notifyIntegrationService = new Mock<INotifyIntegrationService>();
            _skills2DataServiceMock = new();
            _skills2Service = new SkillsTwoService(_logger.Object,_notifyIntegrationService.Object,_skills2DataServiceMock.Object);
        }
        
        [Test]
        public void SaveSkillsTwoResponse_ValidData_Success()
        {
            SkillsTwoResponse response = new SkillsTwoResponse
            {
                UserEmailAddress = "test@test.com",
            
            };
            _skills2Service.SaveSkillsTwoResponse(Guid.NewGuid(), response);
            _skills2DataServiceMock.Verify(x => x.Add(It.IsAny<SkillsTwoResponse>()));
        }

        [Test]
        public void SaveSkillsTwoResponse_NullData_Success()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _skills2Service.SaveSkillsTwoResponse(Guid.NewGuid(), null));
            Assert.That(ex.ParamName == "skillsTwoResponse");
        }
    }
}