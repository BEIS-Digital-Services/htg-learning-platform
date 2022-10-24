using Beis.LearningPlatform.BL.IntegrationServices;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class SkillsOneServiceTests
    {
        private ISkillsOneService _service;
        private Mock<ISkillsOneDataService> _dataService;

        [SetUp]
        public void Setup()
        {
            var logger = new Mock<ILogger<EmailService>>();
            var mapper = new Mock<IMapper>();
            var notifyService = new Mock<INotifyIntegrationService>();
            
            _dataService = new Mock<ISkillsOneDataService>();

            _service = new SkillsOneService(
                logger.Object,
                mapper.Object,
                notifyService.Object,
                _dataService.Object
            );
        }

        [Test]
        public void SaveSkillsOneResponse_NullData_ThrowsException()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() =>
                _service.SaveSkillsOneResponse(Guid.NewGuid(), null)
            );
            
            Assert.That(ex.ParamName, Is.EqualTo("skillsOneResponseDto"));
        }

        [Test]
        public async Task SaveSkillsOneResponse_ValidData_Success()
        {
            var responseInt = 123;
            
            _dataService
                .Setup(d => d.Add(It.IsAny<SkillsOneResponseDto>()))
                .ReturnsAsync(responseInt);

            var requestId = Guid.NewGuid();
            var data = new SkillsOneResponseDto();

            var result = await _service.SaveSkillsOneResponse(requestId, data);
            
            Assert.That(result.RequestID, Is.EqualTo(requestId));
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Message, Is.Null);
            Assert.That(result.Payload, Is.EqualTo(responseInt));
        }
    }
}