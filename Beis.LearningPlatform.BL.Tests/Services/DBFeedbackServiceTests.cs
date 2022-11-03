using Beis.LearningPlatform.BL.Models;
using Beis.LearningPlatform.Data.Entities.Skills;
using Beis.LearningPlatform.Library.DTO;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class DBFeedbackServiceTests
    {
        private IFeedbackService _feedbackService;
        private Mock<IFeedbackProblemReportDataService> _feedbackReportProblemDataService;
        private Mock<IFeedbackUsefulDataService> _feedbackUsefulDataService;
        private Mock<ILogger<DBFeedbackService>> _logger;
        private Mock<IMapper> _mapper;



        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<DBFeedbackService>>();
            _mapper = new Mock<IMapper>();
            _feedbackUsefulDataService = new();
            _feedbackReportProblemDataService = new();
            _feedbackService = new DBFeedbackService(_logger.Object,_mapper.Object,_feedbackReportProblemDataService.Object,_feedbackUsefulDataService.Object);
        }

       
        [Test]
        public void SaveFeedBackPageUseful_ValidData_Success()
        {
            CMSFeedbackPageUsefulBM response = new CMSFeedbackPageUsefulBM
            {
               IsPageUseful = "yes",
               sessionId = "test",
               url = "test.com"
            };
            _mapper.Setup(x => x.Map<FeedbackPageUsefulDto>(It.IsAny<CMSFeedbackPageUsefulBM>())).Returns(new FeedbackPageUsefulDto());

            _feedbackService.SaveFeedBackPageUseful(response);

            _mapper.Verify(x => x.Map<FeedbackPageUsefulDto>(response));
            _feedbackUsefulDataService.Verify(x => x.Add(It.IsAny<FeedbackPageUsefulDto>()));
        }

        [Test]
        public void SaveFeedBackPageUseful_NullData_Test()
        {
            CMSFeedbackPageUsefulBM response = new CMSFeedbackPageUsefulBM
            {
                IsPageUseful = "yes",
                sessionId = "test",
                url = default
            };
            
            var ex = Assert.ThrowsAsync<ApplicationException>(() => _feedbackService.SaveFeedBackPageUseful(response));
            Assert.That(ex.Message == "SaveFeedBackPageUseful missing url");
        }

        [Test]
        public void IsPageUsefull_NullData_Test()
        {
            CMSFeedbackPageUsefulBM response = new CMSFeedbackPageUsefulBM
            {
                sessionId = string.Empty,
                url = string.Empty,
                IsPageUseful = "Test.com"
            };
           var res = _feedbackService.SaveFeedBackPageUseful(response);
            Assert.IsFalse(res.Result);
           
        }

        [Test]
        public void SaveFeedBackReport_ValidData_Success()
        {
            CMSFeedbackProblemBM response = new CMSFeedbackProblemBM
            {
                
                sessionId = "test",
                url = "test.com",
                whatIWasDoing = "test",
                whatWentWrong = "test"
            };
            _mapper.Setup(x => x.Map<FeedbackProblemReportDto>(It.IsAny<CMSFeedbackProblemBM>())).Returns(new FeedbackProblemReportDto());

            _feedbackService.SaveFeedBackReport(response);

            _mapper.Verify(x => x.Map<FeedbackProblemReportDto>(response));
            _feedbackReportProblemDataService.Verify(x => x.Add(It.IsAny<FeedbackProblemReportDto>()));
        }

        [Test]
        public void SaveFeedBackReport_NullData_Test()
        {
            CMSFeedbackProblemBM response = new CMSFeedbackProblemBM
            {
            };
            var ex = Assert.ThrowsAsync<ApplicationException>(() => _feedbackService.SaveFeedBackReport(response));
            Assert.That(ex.Message == "SaveFeedBackReport missing url");
        }

    }
}
