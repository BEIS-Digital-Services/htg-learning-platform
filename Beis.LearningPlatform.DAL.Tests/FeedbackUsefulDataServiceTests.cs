using Beis.LearningPlatform.Data.Entities.Feedback;
using Beis.LearningPlatform.Data.Repositories.Feedback;
using Beis.LearningPlatform.Library.DTO;

namespace Beis.LearningPlatform.DAL.Tests
{
    public class FeedbackUsefulDataServiceTests
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<ILogger<EmailDataService>> _logger;
        private Mock<IMapper> _mapper;
        private Mock<IFeedbackPageUsefulRepository> _repository;

        private IFeedbackUsefulDataService CreateService(bool useLoggerMock = true, bool useMapperMock = true, bool useDataRepositoryMock = true, bool useRepositoryMock = true)
        {
            return new FeedbackUsefulDataService(useLoggerMock ? _logger.Object : default,
                useMapperMock ? _mapper.Object : default,
                useDataRepositoryMock ? _dataRepository.Object : default,
                useRepositoryMock ? _repository.Object : default);
        }
        private static FeedbackPageUsefulDto CreateDto()
        {
            return new FeedbackPageUsefulDto()
            {
                IsPageUseful = true,
                SessionId = "1",
                Url = "Test.com",
               Date = DateTime.Now
               
            };
        }

        [SetUp]
        public void Setup()
        {
            _dataRepository = new Mock<IDataRepository>();
            _logger = new Mock<ILogger<EmailDataService>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<IFeedbackPageUsefulRepository>();

          
        }

        [Test]
        public async Task AddFBEntity_ValidData_Successful1()
        {
            var dto = CreateDto();
            var service = CreateService();
            _mapper.Setup(x => x.Map<FeedbackPageUseful>(It.IsAny<FeedbackPageUsefulDto>())).Returns(new FeedbackPageUseful());

            await service.Add(dto);

            _mapper.Verify(x => x.Map<FeedbackPageUseful>(dto));
            _repository.Verify(x => x.AddAsync(It.IsAny<FeedbackPageUseful>()));
            _dataRepository.Verify(x => x.SaveAsync());
        }
       
        [Test]
        public async Task GetAllEntity_ValidData_Successful()
        {
            //Assets
            var service = CreateService();
            _mapper.Setup(x => x.Map<FeedbackPageUsefulDto[]>(It.IsAny<FeedbackPageUseful[]>()))
                .Returns(new List<FeedbackPageUsefulDto>().ToArray());

            //Arrange
            await service.GetAll();

            //Validate
            _repository.Verify(x => x.GetAllAsync(), Times.Once);
            _mapper.Verify(x => x.Map<FeedbackPageUsefulDto[]>(It.IsAny<FeedbackPageUseful[]>()), Times.Once);
        }

        [Test]
        public async Task UpdateEntity_ValidData_Successful()
        {
            //Assets
            var dto = CreateDto();
            var service = CreateService();
            _mapper.Setup(x => x.Map(It.IsAny<FeedbackPageUsefulDto>(), It.IsAny<FeedbackPageUseful>())).Returns(new FeedbackPageUseful());
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(new FeedbackPageUseful());

            //Arrange
            await service.Update(dto);

            //Validate
            _repository.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
            _mapper.Verify(x => x.Map(It.IsAny<FeedbackPageUsefulDto>(), It.IsAny<FeedbackPageUseful>()), Times.Once);
            _dataRepository.Verify(x => x.SaveAsync());
        }

    }
}