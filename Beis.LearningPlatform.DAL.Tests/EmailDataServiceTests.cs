using Beis.LearningPlatform.Data.Repositories.DiagnosticTool;

namespace Beis.LearningPlatform.DAL.Tests
{
    public class EmailDataServiceTests
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<ILogger<EmailDataService>> _logger;
        private Mock<IMapper> _mapper;
        private Mock<IDiagnosticToolEmailAnswerRepository> _repository;

        private static DiagnosticToolEmailAnswerDto CreateDto()
        {
            return new DiagnosticToolEmailAnswerDto()
            {
                IsOptedInForMarketing = true,
                IsPrivacyPolicyAccepted = true,
                UserEmailAddress = "test@nowhere.com"
            };
        }

        private IEmailDataService CreateService(bool useLoggerMock = true, bool useMapperMock = true, bool useDataRepositoryMock = true, bool useRepositoryMock = true)
        {
            return new EmailDataService(useLoggerMock ? _logger.Object : default,
                                        useMapperMock ? _mapper.Object : default,
                                        useDataRepositoryMock ? _dataRepository.Object : default,
                                        useRepositoryMock ? _repository.Object : default);
        }

        [SetUp]
        public void Setup()
        {
            _dataRepository = new Mock<IDataRepository>();
            _logger = new Mock<ILogger<EmailDataService>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<IDiagnosticToolEmailAnswerRepository>();

            _mapper.Setup(x => x.Map<DiagnosticToolEmailAnswer>(It.IsAny<DiagnosticToolEmailAnswerDto>())).Returns<DiagnosticToolEmailAnswerDto>((source) =>
            {
                return new()
                {
                    IsOptedInForMarketing = source.IsOptedInForMarketing,
                    IsPrivacyPolicyAccepted = source.IsPrivacyPolicyAccepted,
                    UserEmailAddress = source.UserEmailAddress
                };
            });
        }

        [Test]
        public async Task AddEntity_ValidData_Successful()
        {
            var dto = CreateDto();
            var service = CreateService();
            await service.Add(dto);

            _mapper.Verify(x => x.Map<DiagnosticToolEmailAnswer>(dto));
            _repository.Verify(x => x.AddAsync(It.IsAny<DiagnosticToolEmailAnswer>()));
            _dataRepository.Verify(x => x.SaveAsync());
        }
        
        [Test]
        public async Task UpdateEntity_ValidData_Successful()
        {
            var dto = CreateDto();
            var service = CreateService();
            await service.Add(dto);

            dto.IsUnsubscribed = true;

            await service.Update(dto);

            _mapper.Verify(x => x.Map<DiagnosticToolEmailAnswer>(dto));
            _repository.Verify(x => x.Update(It.IsAny<DiagnosticToolEmailAnswer>()));
            _dataRepository.Verify(x => x.SaveAsync());
        }
    }
}