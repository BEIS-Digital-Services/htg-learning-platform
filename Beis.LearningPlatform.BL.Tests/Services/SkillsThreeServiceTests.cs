using Beis.LearningPlatform.Data.Entities.Skills;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class SkillsThreeServiceTests
    {
        private ISkillsThreeService _skills3Service;
        private Mock<ISkillsThreeDataService> _skills3DataServiceMock;

        [SetUp]
        public void Setup()
        {
            _skills3DataServiceMock = new();
            _skills3Service = new SkillsThreeService(_skills3DataServiceMock.Object);
        }

        [Test]
        public void SaveSkillsThreeResponse_NullData_ThrowsException()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _skills3Service.SaveSkillsThreeResponse(Guid.NewGuid(), null));
            Assert.That(ex.ParamName == "skillsThreeResponse");
        }

        [Test]
        public void SaveSkillsThreeResponse_ValidData_Success()
        {
            SkillsThreeResponse response = new SkillsThreeResponse
            {
                UserEmailAddress = "test@test.com",
                Questionnaire = "test",
                WhyNeedStart = "WhyNeedStart test value",
                WhyNeedNext = "WhyNeedNext test value",
                WhyNeedFinally = "WhyNeedFinally test value",
                HowAccessStart = "HowAccessStart test value",
                HowAccessNext = "HowAccessNext test value",
                HowAccessFinally = "HowAccessFinally test value",
                RiskStart = "RiskStart test value",
                RiskNext = "RiskNext test value",
                RiskFinally = "RiskFinally test value",
                UniqueId = "test_id",
            };

            _skills3Service.SaveSkillsThreeResponse(Guid.NewGuid(), response);
            _skills3DataServiceMock.Verify(x => x.Add(It.IsAny<SkillsThreeResponse>()));
        }

        [Test]
        public void FindByUniqueId_WhenCalled_Success()
        {
            _skills3Service.FindByUniqueId(It.IsAny<string>());
            _skills3DataServiceMock.Verify(x => x.FindByUniqueId(It.IsAny<string>()));
        }
    }
}
