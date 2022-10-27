using Beis.LearningPlatform.Data.Entities.Feedback;
using Beis.LearningPlatform.Data.Repositories.Feedback;
using Beis.LearningPlatform.Library.DTO;

namespace Beis.LearningPlatform.DAL.Tests
{
    public class FeedbackProblemReportDataServiceTests
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<ILogger<EmailDataService>> _logger;
        private Mock<IMapper> _mapper;
        private Mock<IFeedbackProblemReportRepository> _repository;

        private IFeedbackProblemReportDataService CreateService(bool useLoggerMock = true, bool useMapperMock = true, bool useDataRepositoryMock = true, bool useRepositoryMock = true)
        {
            return new FeedbackProblemReportDataService(useLoggerMock ? _logger.Object : default,
                useMapperMock ? _mapper.Object : default,
                useDataRepositoryMock ? _dataRepository.Object : default,
                useRepositoryMock ? _repository.Object : default);
        }
        private static FeedbackProblemReportDto CreateDto()
        {
            return new FeedbackProblemReportDto()
            {
                WhatIWasDoing = "Test",
                WhatWentWrong = "Nothing"
               
            };
        }

        [SetUp]
        public void Setup()
        {
            _dataRepository = new Mock<IDataRepository>();
            _logger = new Mock<ILogger<EmailDataService>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<IFeedbackProblemReportRepository>();

          
        }

        [Test]
        public async Task AddFBPEntity_ValidData_Successful()
        {
            var dto = CreateDto();
            var service = CreateService();
           
            _mapper.Setup(x => x.Map<FeedbackProblemReport>(It.IsAny<FeedbackProblemReportDto>())).Returns(new FeedbackProblemReport());

            await service.Add(dto);

            _mapper.Verify(x => x.Map<FeedbackProblemReport>(dto));
            _repository.Verify(x => x.AddAsync(It.IsAny<FeedbackProblemReport>()));
            _dataRepository.Verify(x => x.SaveAsync());
        }

        [Test]
        public async Task GetAllEntity_ValidData_Successful()
        {
            //Assets
            var service = CreateService();
            _mapper.Setup(x => x.Map<FeedbackProblemReportDto[]>(It.IsAny<FeedbackProblemReport[]>()))
                .Returns(new List<FeedbackProblemReportDto>().ToArray());

            //Arrange
            await service.GetAll();

            //Validate
            _repository.Verify(x => x.GetAllAsync(), Times.Once);
            _mapper.Verify(x => x.Map<FeedbackProblemReportDto[]>(It.IsAny<FeedbackProblemReport[]>()), Times.Once);
        }

        [Test]
        public async Task UpdateEntity_ValidData_Successful()
        {
            //Assets
            var dto = CreateDto();
            var service = CreateService();
            _mapper.Setup(x => x.Map(It.IsAny<FeedbackProblemReportDto>(), It.IsAny<FeedbackProblemReport>())).Returns(new FeedbackProblemReport());
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(new FeedbackProblemReport());

            //Arrange
            await service.Update(dto);

            //Validate
            _repository.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
            _mapper.Verify(x => x.Map(It.IsAny<FeedbackProblemReportDto>(), It.IsAny<FeedbackProblemReport>()), Times.Once);
            _dataRepository.Verify(x => x.SaveAsync());
        }

    }
}