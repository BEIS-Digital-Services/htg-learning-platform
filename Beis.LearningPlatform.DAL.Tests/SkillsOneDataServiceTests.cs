using Beis.LearningPlatform.Data.Entities.Skills;
using Beis.LearningPlatform.Data.Repositories.Skills;

namespace Beis.LearningPlatform.DAL.Tests
{
    public class SkillsOneDataServiceTests
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<ILogger<EmailDataService>> _logger;
        private Mock<IMapper> _mapper;
        private Mock<ISkillsOneResponseRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _dataRepository = new Mock<IDataRepository>();
            _logger = new Mock<ILogger<EmailDataService>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<ISkillsOneResponseRepository>();

            _mapper
                .Setup(x => x.Map<SkillsOneResponse>(It.IsAny<SkillsOneResponseDto>()))
                .Returns<SkillsOneResponseDto>((_) => new SkillsOneResponse());
        }

        [Test]
        public async Task AddEntity_ValidData_Successful()
        {
            var dto = new SkillsOneResponseDto();
            var service = new SkillsOneDataService(
                _logger.Object,
                _mapper.Object,
                _dataRepository.Object,
                _repository.Object
            );
            
            await service.Add(dto);

            _mapper.Verify(x => x.Map<SkillsOneResponse>(dto));
            _repository.Verify(x => x.AddAsync(It.IsAny<SkillsOneResponse>()));
            _dataRepository.Verify(x => x.SaveAsync());
        }
    }
}