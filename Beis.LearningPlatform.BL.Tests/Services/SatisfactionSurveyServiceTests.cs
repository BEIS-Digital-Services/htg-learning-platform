using AutoMapper;
using Beis.LearningPlatform.BL.Services;
using Beis.LearningPlatform.DAL;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class SatisfactionSurveyServiceTests
    {
        private Mock<ISatisfactionSurveyDataService> _satisfactionSurveyDataServiceMock;
        private ISatisfactionSurveyService _satisfactionSurveyService;
        private Mock<ILogger<SatisfactionSurveyService>> _loggerMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _satisfactionSurveyDataServiceMock = new();
            _loggerMock = new();
            _mapperMock = new();

            _satisfactionSurveyService = new SatisfactionSurveyService(_loggerMock.Object, _mapperMock.Object, _satisfactionSurveyDataServiceMock.Object);
        }

        [Test]
        public void CreateInstance_ValidParameters_IsSuccessful()
        {
            Assert.IsNotNull(_satisfactionSurveyDataServiceMock);
        }

        [Test]
        public void SaveEmailAddress_NullDto_ThrowsException()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _satisfactionSurveyService.SaveSatisfactionSurvey(Guid.NewGuid(), null));
            Assert.That(ex.ParamName == "satisfactionSurveyDto");
        }
    }
}