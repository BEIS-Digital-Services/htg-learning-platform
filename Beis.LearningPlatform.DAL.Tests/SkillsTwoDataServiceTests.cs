using Beis.LearningPlatform.Data.Entities.Skills;
using Beis.LearningPlatform.Data.Repositories.Skills;

namespace Beis.LearningPlatform.DAL.Tests
{
    public class SkillsTwoDataServiceTests
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<ILogger<EmailDataService>> _logger;
        private Mock<IMapper> _mapper;
        private Mock<ISkillsTwoResponseRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _dataRepository = new Mock<IDataRepository>();
            _logger = new Mock<ILogger<EmailDataService>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<ISkillsTwoResponseRepository>();
        }

        [Test]
        public async Task AddEntity_ValidData_Successful()
        {
            var dto = new SkillsTwoResponse();
            var service = new SkillsTwoDataService(
                _logger.Object,
                _mapper.Object,
                _dataRepository.Object,
                _repository.Object
            );
            
            await service.Add(dto);

            _repository.Verify(x => x.AddAsync(It.IsAny<SkillsTwoResponse>()));
            _dataRepository.Verify(x => x.SaveAsync());
        }
    }
}