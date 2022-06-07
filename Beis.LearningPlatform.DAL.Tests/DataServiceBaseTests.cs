namespace Beis.LearningPlatform.DAL.Tests
{
    public class DataServiceBaseTests
    {
        private Mock<ILogger<DataServiceBase>> _logger;
        private Mock<IMapper> _mapper;

        private DataServiceBaseMock CreateService(bool useLoggerMock = true, bool useMapperMock = true)
        {
            return new DataServiceBaseMock(useLoggerMock ? _logger.Object : default,
                                           useMapperMock ? _mapper.Object : default);
        }

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<DataServiceBase>>();
            _mapper = new Mock<IMapper>();
        }

        [Test]
        public void CreateInstance_WithLogger_ProtectedLoggerIsSet()
        {
            var service = CreateService();
            Assert.AreEqual(service.Logger, _logger.Object);
        }

        [Test]
        public void CreateInstance_WithMapper_ProtectedMapperIsSet()
        {
            var service = CreateService();
            Assert.AreEqual(service.Mapper, _mapper.Object);
        }
    }
}