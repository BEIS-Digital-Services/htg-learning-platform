using AutoMapper;
using Beis.LearningPlatform.Data.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Beis.LearningPlatform.DAL.Tests
{
    public class RepositoryDataServiceBaseTests
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<ILogger<DataServiceBase>> _logger;
        private Mock<IMapper> _mapper;
        private Mock<IFakeRepository> _repository;

        private RepositoryDataServiceBaseMock CreateService(bool useLoggerMock = true, bool useMapperMock = true, bool useDataRepositoryMock = true, bool useRepositoryMock = true)
        {
            return new RepositoryDataServiceBaseMock(useLoggerMock ? _logger.Object : default,
                                                     useMapperMock ? _mapper.Object : default,
                                                     useDataRepositoryMock ? _dataRepository.Object : default,
                                                     useRepositoryMock ? _repository.Object : default);
        }

        [SetUp]
        public void Setup()
        {
            _dataRepository = new Mock<IDataRepository>();
            _logger = new Mock<ILogger<DataServiceBase>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<IFakeRepository>();
        }

        [Test]
        public void CreateInstance_WithDataRepository_ProtectedDataRepositoryIsSet()
        {
            var service = CreateService();
            Assert.AreEqual(service.DataRepository, _dataRepository.Object);
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

        [Test]
        public void CreateInstance_WithRepository_ProtectedRepositoryIsSet()
        {
            var service = CreateService();
            Assert.AreEqual(service.Repository, _repository.Object);
        }

        [Test]
        public void Service_Save_ProtectedSaveIsExecuted()
        {
            var service = CreateService();
            service.SaveMock();
            _dataRepository.Verify(x => x.Save());
        }

        [Test]
        public void Service_SaveAsync_ProtectedSaveAsyncIsExecuted()
        {
            var service = CreateService();
            service.SaveAsyncMock();
            _dataRepository.Verify(x => x.SaveAsync());
        }
    }
}