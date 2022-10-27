using Beis.LearningPlatform.Data.Entities;
using Beis.LearningPlatform.Data.Entities.Feedback;
using Beis.LearningPlatform.Data.Repositories.ExpressionOfInterest;
using Beis.LearningPlatform.Data.Repositories.Feedback;
using Beis.LearningPlatform.Library.DTO;

namespace Beis.LearningPlatform.DAL.Tests
{
    public class ExpressionOfInterestDataServiceTests
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<ILogger<EmailDataService>> _logger;
        private Mock<IMapper> _mapper;
        private Mock<IExpressionOfInterestRepository> _repository;

        private IExpressionOfInterestDataService CreateService(bool useLoggerMock = true, bool useMapperMock = true, bool useDataRepositoryMock = true, bool useRepositoryMock = true)
        {
            return new ExpressionOfInterestDataService(useLoggerMock ? _logger.Object : default,
                useMapperMock ? _mapper.Object : default,
                useDataRepositoryMock ? _dataRepository.Object : default,
                useRepositoryMock ? _repository.Object : default);
        }
        private static ExpressionOfInterestDto CreateDto()
        {
            return new ExpressionOfInterestDto()
            {
                UserEmail = "Test@test.com",
                PageName = "Test Page",
                UserName = "TestUser",
                UserPhone = "99864512",
                UserBusinessName = "Test Business",
                RecordCreatedUtc = DateTime.Now
            };
        }

        [SetUp]
        public void Setup()
        {
            _dataRepository = new Mock<IDataRepository>();
            _logger = new Mock<ILogger<EmailDataService>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<IExpressionOfInterestRepository>();
        }

        [Test]
        public async Task AddEIDEntity_ValidData_Successful()
        {
            var dto = CreateDto();
            var service = CreateService();
            _mapper.Setup(x => x.Map<ExpressionOfInterest>(It.IsAny<ExpressionOfInterestDto>())).Returns(new ExpressionOfInterest());
            await service.Add(dto);
            
            _repository.Verify(x => x.AddAsync(It.IsAny<ExpressionOfInterest>()));
            _dataRepository.Verify(x => x.SaveAsync());
        }

    }
}