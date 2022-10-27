using Beis.LearningPlatform.BL.IntegrationServices;
using Beis.LearningPlatform.Data.Entities.Skills;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class SkillsOneServiceTests
    {
        private ISkillsOneService _skills1Service;
        private Mock<ILogger<EmailService>> _logger;
        private Mock<INotifyIntegrationService> _notifyIntegrationService;
        private Mock<ISkillsOneDataService> _skills1DataServiceMock;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<EmailService>>();
            _mapper = new Mock<IMapper>();
            _notifyIntegrationService = new Mock<INotifyIntegrationService>();
            _skills1DataServiceMock = new();
            _skills1Service = new SkillsOneService(_logger.Object,_mapper.Object,_notifyIntegrationService.Object, _skills1DataServiceMock.Object);
        }
        
        [Test]
        public void SaveSkillsOneResponse_ValidData_Success()
        {
            SkillsOneResponseDto response = new SkillsOneResponseDto
            {
                UserEmailAddress = "test@test.com",
                
            };
            _skills1Service.SaveSkillsOneResponse(Guid.NewGuid(), response);
            _skills1DataServiceMock.Verify(x => x.Add(It.IsAny<SkillsOneResponseDto>()));
        }

        [Test]
        public void SaveSkillsOneResponse_NullData_Success()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _skills1Service.SaveSkillsOneResponse(Guid.NewGuid(), null));
            Assert.That(ex.ParamName == "skillsOneResponseDto");
        }
    }
}
