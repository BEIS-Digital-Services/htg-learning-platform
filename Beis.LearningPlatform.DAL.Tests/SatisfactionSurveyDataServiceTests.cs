using Beis.LearningPlatform.Data.Repositories.SatisfactionSurvey;

namespace Beis.LearningPlatform.DAL.Tests
{
    public class SatisfactionSurveyDataServiceTests
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<ILogger<SatisfactionSurveyDataService>> _logger;
        private Mock<IMapper> _mapper;
        private Mock<ISatisfactionSurveyRepository> _repository;

        private SatisfactionSurveyDto CreateDto()
        {
            return new SatisfactionSurveyDto()
            {
                Rating = "My rating",
                Comment = "My Comment"
            };
        }

        private ISatisfactionSurveyDataService CreateService(bool useLoggerMock = true, bool useMapperMock = true, bool useDataRepositoryMock = true, bool useRepositoryMock = true)
        {
            return new SatisfactionSurveyDataService(useLoggerMock ? _logger.Object : default,
                                        useMapperMock ? _mapper.Object : default,
                                        useDataRepositoryMock ? _dataRepository.Object : default,
                                        useRepositoryMock ? _repository.Object : default);
        }

        [SetUp]
        public void Setup()
        {
            _dataRepository = new Mock<IDataRepository>();
            _logger = new Mock<ILogger<SatisfactionSurveyDataService>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<ISatisfactionSurveyRepository>();

            _mapper.Setup(x => x.Map<SatisfactionSurveyEntry>(It.IsAny<SatisfactionSurveyDto>())).Returns<SatisfactionSurveyDto>((source) =>
            {
                return new()
                {
                    rating = source.Rating,
                    comment = source.Comment
                };
            });
        }

        [Test]
        public async Task AddEntity_ValidData_Successful()
        {
            var dto = CreateDto();
            var service = CreateService();
            await service.Add(dto);

            _mapper.Verify(x => x.Map<SatisfactionSurveyEntry>(dto));
            _repository.Verify(x => x.AddAsync(It.IsAny<SatisfactionSurveyEntry>()));
            _dataRepository.Verify(x => x.SaveAsync());
        }
    }
}