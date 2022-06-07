using Beis.LearningPlatform.BL.IntegrationServices;
using Beis.LearningPlatform.BL.IntegrationServices.Options;
using Beis.LearningPlatform.BL.Models;
using Microsoft.Extensions.Options;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class EmailServiceTests
    {
        private Mock<IEmailDataService> _emailDataServiceMock;
        private IEmailService _emailService;
        private Mock<ILogger<EmailService>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private Mock<INotifyIntegrationService> _notifyIntegrationService;
        private IOptions<NotifyServiceOption> _options;
        private const string TestEmailAddress = "testuser@test.com";

        private DiagnosticToolEmailAnswerDto CreateDto(bool acceptPrivacy, bool acceptMarketing)
        {
            return new()

            {
                IsOptedInForMarketing = acceptMarketing,
                IsPrivacyPolicyAccepted = acceptPrivacy,
                UserEmailAddress = TestEmailAddress
            };
        }

        private DiagnosticToolResultsEmailDataDto CreateDto_ResultsEmailData()
        {
            return new()
            {
                Id = 0,
                BusinessSector = "Accommodation and Food Services",
                Question6Answer = "Yes",
                SoftwareChoices = "CRM Software",
                SoftwareNeed = "",
                StreamlineTasks = "",
                TradeMethod = "",
                RecommendedSoftware = "",
            };
        }

        private DiagnosticToolEmailAnswerDto[] GetByEmail_Result(string emailAddress, bool? isUnsubscribed)
        {
            return new List<DiagnosticToolEmailAnswerDto>()
            {
                new()
                {
                    UserEmailAddress = emailAddress,
                    IsUnsubscribed=isUnsubscribed,
                }
            }.ToArray();
        }

        [SetUp]
        public void Setup()
        {
            _emailDataServiceMock = new();
            _loggerMock = new();
            _mapperMock = new();
            _notifyIntegrationService = new();
            _options = Options.Create(new NotifyServiceOption { Templates = new Templates { DtResultPageQ6Yes = Guid.NewGuid().ToString(), DtResultPageQ6No = Guid.NewGuid().ToString() } });

            _emailService = new EmailService(_loggerMock.Object, _mapperMock.Object, _notifyIntegrationService.Object, 
                _emailDataServiceMock.Object, _options);

            _mapperMock.Setup(x => x.Map<DiagnosticToolResultsEmailData>(It.IsAny<DiagnosticToolResultsEmailDataDto>())).Returns<DiagnosticToolResultsEmailDataDto>((source) => new());
        }

        [Test]
        public void CreateInstance_ValidParameters_IsSuccessful()
        {
            Assert.IsNotNull(_emailService);
        }

        [TestCase("plainAddress")]
        [TestCase("#@%^%#$@#$@#.com")]
        [TestCase("@example.com")]
        [TestCase("Joe Smith <email@example.com>")]
        [TestCase("email.example.com")]
        [TestCase("email@example@example.com")]
        [TestCase(".email@example.com")]
        [TestCase("email@example.com (Joe Smith)")]
        [TestCase("”(),:;<>[\\]@example.com")]
        [TestCase("this\\ is\"really\"not\\allowed@example.com")]
        [TestCase("test@email")]
        public void IsValidEmailAddress_InvalidEmails_AllInvalid(string emailAddress)
        {
            Assert.IsFalse(_emailService.IsValidEmailAddress(Guid.NewGuid(), emailAddress).IsValid);
        }

        [Test]
        public void IsValidEmailAddress_NullEmails_IsInvalid()
        {
            Assert.IsFalse(_emailService.IsValidEmailAddress(Guid.NewGuid(), null).IsValid);
        }

        [TestCase("email@example.com")]
        [TestCase("firstname.lastname@example.com")]
        [TestCase("email@subdomain.example.com")]
        [TestCase("firstname+lastname@example.com")]
        [TestCase("email@123.123.123.123")]
        [TestCase("email@[123.123.123.123]")]
        [TestCase("\"email\"@example.com")]
        [TestCase("1234567890@example.com")]
        [TestCase("email@example-one.com")]
        [TestCase("_______@example.com")]
        [TestCase("email@example.name")]
        [TestCase("email@example.museum")]
        [TestCase("email@example.co.jp")]
        [TestCase("firstname-lastname@example.com")]
        public void IsValidEmailAddress_ValidEmails_AllValid(string emailAddress)
        {
            Assert.IsTrue(_emailService.IsValidEmailAddress(Guid.NewGuid(), emailAddress).IsValid);
        }


        [Test]
        public void SaveEmailAddress_NullDto_ThrowsException()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _emailService.SaveEmailAddress(Guid.NewGuid(), null));
            Assert.That(ex.ParamName == "diagnosticToolEmailAnswer");
        }

        [Test]
        public void SaveEmailAddress_ValidParametersWithMarketing_Succeeds()
        {
            _emailService.SaveEmailAddress(Guid.NewGuid(), CreateDto(true, true));
            _emailDataServiceMock.Verify(x => x.Add(It.IsAny<DiagnosticToolEmailAnswerDto>()));
        }

        [Test]
        public void SaveEmailAddress_ValidParametersWithoutMarketing_Succeeds()
        {
            _emailService.SaveEmailAddress(Guid.NewGuid(), CreateDto(true, false));
            _emailDataServiceMock.Verify(x => x.Add(It.IsAny<DiagnosticToolEmailAnswerDto>()));
        }

        [Test]
        public async Task IsUnsubscribed_UnsubscribedEmail_ReturnsPayloadTrue()
        {
            _emailDataServiceMock.Setup(r => r.GetByEmail(TestEmailAddress)).Returns(Task.FromResult(GetByEmail_Result(TestEmailAddress, true)));
            var result = await _emailService.IsUnsubscribed(Guid.NewGuid(), TestEmailAddress);
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Payload, Is.True);
        }

        [Test]
        public async Task IsUnsubscribed_ValidSubscribedEmail_ReturnsPayloadFalse()
        {
            _emailDataServiceMock.Setup(r => r.GetByEmail(TestEmailAddress)).Returns(Task.FromResult(GetByEmail_Result(TestEmailAddress, default)));
            var result = await _emailService.IsUnsubscribed(Guid.NewGuid(), TestEmailAddress);
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.Payload, Is.False);
        }

        [Test]
        public async Task IsUnsubscribed_EmailNotExist_ReturnsUnsuccessful()
        {
            var getByEmailResult = new List<DiagnosticToolEmailAnswerDto>() { }.ToArray();
            _emailDataServiceMock.Setup(r => r.GetByEmail(TestEmailAddress)).Returns(Task.FromResult(getByEmailResult));
            var result = await _emailService.IsUnsubscribed(Guid.NewGuid(), TestEmailAddress);
            Assert.That(result.IsValid, Is.False);
        }

        [Test]
        public void SendResultsRemail_NullDto_ThrowsException()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _emailService.SendResultsRemail(Guid.NewGuid(), TestEmailAddress, It.IsAny<IEmailDto>()));
            Assert.That(ex.ParamName == nameof(IEmailDto));
        }
        [Test]
        public async Task SendResultsRemail_InvalidEmail_Unsuccessful()
        {
            var result = await _emailService.SendResultsRemail(Guid.NewGuid(), "TestUser", CreateDto_ResultsEmailData());
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Message.ToLower().Contains("invalid"));
        }

        [Test]
        public async Task SendResultsRemail_UnsubscribedEmail_Unsuccessful()
        {
            _emailDataServiceMock.Setup(r => r.GetByEmail(TestEmailAddress)).Returns(Task.FromResult(GetByEmail_Result(TestEmailAddress, true)));
            var result = await _emailService.SendResultsRemail(Guid.NewGuid(), TestEmailAddress, CreateDto_ResultsEmailData());
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Message.ToLower().Contains("unsubscribed"));
        }

        [Test]
        public async Task SendResultsRemail_ValidSubscribedEmail_Success()
        {
            var templateId = Guid.NewGuid().ToString();
            var getByEmailResult = GetByEmail_Result(TestEmailAddress, default);
            var dto = CreateDto_ResultsEmailData();

            _emailDataServiceMock.Setup(e => e.GetByEmail(TestEmailAddress)).Returns(Task.FromResult(getByEmailResult));
           _notifyIntegrationService.Setup(n => n.SendDiagnosticToolResult(TestEmailAddress, templateId, It.IsAny<Dictionary<string, dynamic>>())).Verifiable();

            _mapperMock.Setup(x => x.Map<DiagnosticToolResultsEmailData>(It.IsAny<DiagnosticToolResultsEmailDataDto>()))
                .Returns<DiagnosticToolResultsEmailDataDto>((source) =>
            {
                return new DiagnosticToolResultsEmailData
                {
                    BusinessSector = source.BusinessSector,
                    TradeMethod = source.TradeMethod,
                    SoftwareNeed = source.SoftwareNeed,
                    SoftwareChoices = source.SoftwareChoices,
                    StreamlineTasks = source.StreamlineTasks,
                    RecommendedSoftware = source.RecommendedSoftware,
                    RelatedArticles = new List<DiagnosticToolResultsEmailRelatedArticle>().ToArray()
                };
            });


             var result = await _emailService.SendResultsRemail(Guid.NewGuid(), TestEmailAddress, dto);
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public async Task UnsubscribeEmail_EmailNotExist_Unsuccessful()
        {
            var getByEmailResult = new List<DiagnosticToolEmailAnswerDto>() { }.ToArray();
            _emailDataServiceMock.Setup(r => r.GetByEmail(TestEmailAddress)).Returns(Task.FromResult(getByEmailResult));
            var result = await _emailService.UnsubscribeEmail(Guid.NewGuid(), TestEmailAddress);
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Message.ToLower().Contains("does not exist"));
        }

        [Test]
        public async Task UnsubscribeEmail_ValidEmail_Successful()
        {
            _emailDataServiceMock.Setup(r => r.GetByEmail(TestEmailAddress)).ReturnsAsync(GetByEmail_Result(TestEmailAddress, default));

            var inputDto = new DiagnosticToolEmailAnswerDto
            {
                UserEmailAddress = TestEmailAddress,
                IsUnsubscribed = default,
            };
            _emailDataServiceMock.Setup(r => r.Update(inputDto)).Verifiable();

            var result = await _emailService.UnsubscribeEmail(Guid.NewGuid(), TestEmailAddress);
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public async Task UnsubscribeEmail_ThrowExceptionOnUpdate_UnSuccessful()
        {
            var inputDto = new DiagnosticToolEmailAnswerDto
            {
                UserEmailAddress = TestEmailAddress,
                IsUnsubscribed = default
            };
            _emailDataServiceMock.Setup(r => r.GetByEmail(TestEmailAddress)).ReturnsAsync(new[] { inputDto });

            _emailDataServiceMock.Setup(fd => fd.Update(inputDto)).ThrowsAsync(new Exception("message"));
            
            var result = await _emailService.UnsubscribeEmail(Guid.NewGuid(), TestEmailAddress);
            
            Assert.That(result.IsValid, Is.False);
        }

        [Test]
        public async Task UnsubscribeEmail_AlreadyUnsubscribed_Unsuccessful()
        {
            _emailDataServiceMock.Setup(r => r.GetByEmail(TestEmailAddress)).Returns(Task.FromResult(GetByEmail_Result(TestEmailAddress, true)));
            var result = await _emailService.UnsubscribeEmail(Guid.NewGuid(), TestEmailAddress);
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Message.ToLower().Contains("already unsubscribed"));
        }
    }
}