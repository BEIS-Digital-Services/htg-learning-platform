using Beis.LearningPlatform.Data.Entities.Skills;
using Beis.LearningPlatform.Data.Repositories.Skills;

namespace Beis.LearningPlatform.DAL.Tests
{
    public class SkillsThreeDataServiceTests
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<ILogger<EmailDataService>> _logger;
        private Mock<IMapper> _mapper;
        private Mock<ISkillsThreeResponseRepository> _repository;
        private SkillsThreeDataService _service;
        
        [SetUp]
        public void Setup()
        {
            _dataRepository = new Mock<IDataRepository>();
            _logger = new Mock<ILogger<EmailDataService>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<ISkillsThreeResponseRepository>();
            _service = new SkillsThreeDataService(
                _logger.Object,
                _mapper.Object,
                _dataRepository.Object,
                _repository.Object
            );
        }

        [Test]
        public async Task AddEntity_ValidData_Successful()
        {
            var dto = new SkillsThreeResponse();
            
            await _service.Add(dto);

            _repository.Verify(x => x.Update(It.IsAny<SkillsThreeResponse>()));
            _dataRepository.Verify(x => x.SaveAsync());
        }

        [Test]
        public void FindByUniqueId_Successful()
        {
            var dto = new SkillsThreeResponse();
            
            _repository
                .Setup(x => x.FindByUniqueId(It.IsAny<string>()))
                .Returns(dto);

            var result = _service.FindByUniqueId("unique id");
            
            _repository.Verify(x => x.FindByUniqueId(It.IsAny<string>()));
            
            Assert.That(result, Is.EqualTo(dto));
        }
    }
}