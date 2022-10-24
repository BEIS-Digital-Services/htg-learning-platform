using Beis.LearningPlatform.Data.Entities.Skills;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ControllerHelperTests
{
    public class DiagnosticToolControllerHelperTests
    {
        private IDiagnosticToolControllerHelper _diagnosticToolControllerHelper;
        private Mock<ILogger<DiagnosticToolControllerHelper>> _logger;
        private IDiagnosticToolFormService _diagnosticToolFormService;
        private Mock<ILogger<DiagnosticToolFormService>> _loggerDiagnosticToolFormService;
        private Mock<ICmsService2> _cmsService;
        private Mock<IMapper> _mapper;
        private Mock<IEmailService> _emailService;
        private Mock<ISkillsOneService> _skillsOneService;
        private Mock<ISkillsTwoService> _skillsTwoService;
        private Mock<ISkillsThreeService> _skillsThreeService;
        private IOptions<VendorAppOption> _vendorAppOptions;
        private IOptions<ComparisonToolDisplayOption> _ctDisplayOptions;
        private IOptions<ApplicationForm> _applicationFormOptions;
        private Mock<IEmailResponseHelperFactory> _emailResponseHelperFactory;
        private Mock<IEmailResponseHelper> _emailResponseHelper;
        private Mock<IEmailDto> _emailDto;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new();
        private readonly Mock<HttpContext> _httpContext = new();
        private readonly Mock<HttpRequest> _httpRequest = new();

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<DiagnosticToolControllerHelper>>();
            _loggerDiagnosticToolFormService = new Mock<ILogger<DiagnosticToolFormService>>();
            _vendorAppOptions = ConfigOptions.Create(new VendorAppOption { ProdLogoUrl = "https://www.vendorapp.com/logo" });
            _ctDisplayOptions = ConfigOptions.Create(new ComparisonToolDisplayOption());
            _applicationFormOptions = ConfigOptions.Create(new ApplicationForm());
            
            _diagnosticToolFormService = new DiagnosticToolFormService(_loggerDiagnosticToolFormService.Object, _ctDisplayOptions, _applicationFormOptions);
            _cmsService = new Mock<ICmsService2>();
            _mapper = new Mock<IMapper>();
            _emailService = new Mock<IEmailService>();
            _skillsOneService = new Mock<ISkillsOneService>();
            _skillsTwoService = new Mock<ISkillsTwoService>();
            _skillsThreeService = new Mock<ISkillsThreeService>();

            _emailResponseHelperFactory = new Mock<IEmailResponseHelperFactory>();
            _emailResponseHelper = new Mock<IEmailResponseHelper>();
            _emailDto =  new Mock<IEmailDto>();
            _httpRequest.SetupGet(x => x.Path)
                .Returns("/diagnostic-tool-controller-helper/tests");
            _httpRequest.SetupGet(x => x.Method)
                .Returns("POST");
            _httpContext.SetupGet(x => x.Request)
                .Returns(_httpRequest.Object);
            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);

            _diagnosticToolControllerHelper = new DiagnosticToolControllerHelper(_logger.Object,
                                                                                    _diagnosticToolFormService,
                                                                                    _cmsService.Object,
                                                                                    _mapper.Object,
                                                                                    _emailService.Object,
                                                                                    _skillsOneService.Object,
                                                                                    _skillsTwoService.Object,
                                                                                    _skillsThreeService.Object,
                                                                                    _vendorAppOptions,
                                                                                    _emailResponseHelperFactory.Object,
                                                                                    _httpContextAccessor.Object);
        }

        #region Private Methods

        private IServiceResponse GetServiceResponse_Result(System.Guid requestId, bool isValid)
        {
            return new ServiceResponse(requestId, isValid);
        }
        private IServiceResponse GetServiceResponse_Result(Guid requestID, bool isValid, string message)
        {
            return new ServiceResponse(requestID, isValid, message);
        }
        private IServiceResponse<int> GetServiceResponse_Result(Guid requestID, bool isValid, string message, int payload)
        {
            return new ServiceResponse<int>(requestID, isValid, message, payload);
        }
        private DiagnosticToolForm CreateForm()
        {
            //create new DiagnosticToolForm can be manual 
            //or by calling following method
            var form = _diagnosticToolControllerHelper.CreateForm(FormTypes.DiagnosticTool).Result;
            return form.Payload;
        }

        private DiagnosticToolForm GetFormWithStep(int currentStep, out string[] errMessages, out int nextStep, bool step6SoftwareNeedKnown = false)
        {
            errMessages = new string[0];

            if ((currentStep == 6 && step6SoftwareNeedKnown == false) || currentStep == 7)
                nextStep = currentStep + 2;
            else
                nextStep = currentStep + 1;

            //create form
            var form = CreateForm();

            //set current step
            form.CurrStep = currentStep;

            if (currentStep == 1)
            {
                form.steps[0].elements[0].value = "Online";
            }
            else if (currentStep == 2)
            {
                form.steps[1].elements[0].selectedValue = "Education";
            }
            else if (currentStep == 3)
            {
                form.steps[2].elements[0].value = "We use some digital methods and want more.";
            }
            else if (currentStep == 4)
            {
                form.steps[3].elements[0].value = "We rely on supplier support.";
            }
            else if (currentStep == 5)
            {
                form.steps[4].elements[0].answerOptions[0].value = "true";
            }
            else if (currentStep == 6)
            {
                if (step6SoftwareNeedKnown == true)
                    form.steps[5].elements[0].value = "Yes";
                else
                    form.steps[5].elements[0].value = "No";
            }
            else if (currentStep == 7)
            {
                //step 7 only displays if in last step value is "Yes", so make last step value to "Yes"
                form.steps[5].elements[0].value = "Yes";

                form.steps[6].elements[0].answerOptions[0].value = "true";
            }
            else if (currentStep == 8)
            {
                form.steps[7].elements[0].answerOptions[0].value = "true";
            }


            return form;
        }

        #endregion Private Methods

        [Test]
        public async Task CreateForm_WhenCalled_ShouldReturnForm()
        {
            //Act
            var result = await _diagnosticToolControllerHelper.CreateForm(FormTypes.DiagnosticTool);

            //Assert
            Assert.That(result.Result, Is.True);

        }

        [Test]
        public async Task Start_WhenCalled_ShouldReturnTrue()
        {
            //Arrange
            var form = CreateForm();

            //Act
            var result = await _diagnosticToolControllerHelper.Start(form);

            //Assert
            Assert.That(result.Result, Is.True);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6, true)]
        [TestCase(6)]
        [Test]
        public async Task NextStep_WhenCalled_ShouldReturnResultTrue(int currentStep, bool step6SoftwareNeedKnown = false)
        {
            //Arrange
            var form = GetFormWithStep(currentStep, out var errMessages, out var nextStep, step6SoftwareNeedKnown);

            //Act
            var result = await _diagnosticToolControllerHelper.NextStep(form, false, errMessages);

            //Assert
            Assert.That(result.Result, Is.True);
            Assert.That(result.Payload, Is.Not.Null);
            Assert.That(result.Payload.validationErrors.Count, Is.EqualTo(0));
            Assert.That(result.Payload.CurrStep, Is.EqualTo(nextStep));
        }

        [TestCase(7)]
        [TestCase(8)]
        [Test]
        public async Task NextStep_LastStep_ShouldReturnCompleted(int currentStep)
        {
            //Arrange
            var form = GetFormWithStep(currentStep, out var errMsg, out var nextStep);

            //Act
            var result = await _diagnosticToolControllerHelper.NextStep(form, true, errMsg);

            //Assert
            Assert.That(result.Payload.FormIsCompleted, Is.True);
        }

        [Test]
        public async Task NextStep_TestStep1WithIncompleteData_ShouldBeValidationError()
        {
            //Arrange
            var form = CreateForm();
            form.CurrStep = 1;
            string[] errMessages = { };
            //Act
            var result = await _diagnosticToolControllerHelper.NextStep(form, false, errMessages);

            //Assert
            Assert.That(result.Payload, Is.Not.Null);
            Assert.That(result.Payload.CurrStep, Is.EqualTo(1));
            Assert.That(result.Payload.validationErrors.Count, Is.GreaterThan(0));

        }

        [Test]
        public async Task PreviousStep_FromStep2_CurrentStepShouldBe1()
        {
            //Arrange
            var form = CreateForm();
            form.CurrStep = 2;
            string[] errMessages = { };
            //Act
            var result = await _diagnosticToolControllerHelper.PreviousStep(form);

            //Assert
            Assert.That(result.Payload, Is.Not.Null);
            Assert.That(result.Payload.CurrStep, Is.EqualTo(1));

        }

        [Test]
        public async Task PreviousStep_FromStep8_CurrentStepShouldBe6()
        {
            //Arrange
            var form = CreateForm();
            form.CurrStep = 8;

            //make step 7 skippable, so if we go back from step 8 we should get to step 6
            form.steps[6].skipStep = true;

            string[] errMessages = { };
            //Act
            var result = await _diagnosticToolControllerHelper.PreviousStep(form);

            //Assert
            Assert.That(result.Payload, Is.Not.Null);
            Assert.That(result.Payload.CurrStep, Is.EqualTo(6));

        }

        [Test]
        public async Task GotoStep_FromStep8To3_CurrentStepShouldBe3()
        {
            //Arrange
            var form = CreateForm();
            form.CurrStep = 8;

            string[] errMessages = { };
            //Act
            var result = await _diagnosticToolControllerHelper.GotoStep(form, 3);

            //Assert
            Assert.That(result.Payload.CurrStep, Is.EqualTo(3));

        }


        [Test]
        public async Task ProcessResults_ValidEmailAndPrivacyPolicyNotAccepted_ShouldBeInvalid()
        {
            //Arrange
            var form = GetFormWithStep(8, out var errMsg, out var nextStep);
            form.EmailAnswer.UserEmailAddress = "test@test.com";
            form.EmailAnswer.IsPrivacyPolicyAccepted = false;

            //Act
            var result = await _diagnosticToolControllerHelper.ProcessResults(form, FormTypes.DiagnosticTool);

            //Assert
            Assert.That(result.Payload, Is.False);
            Assert.That(form.validationErrors.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task ProcessResults_InValidEmailAndPrivacyPolicyAccepted_ShouldBeInvalid()
        {
            //Arrange
            var invalidEmailAddress = "AnInvalidEmailAddress";
            var form = GetFormWithStep(8, out var errMsg, out var nextStep);
            form.EmailAnswer.UserEmailAddress = invalidEmailAddress;
            form.EmailAnswer.IsPrivacyPolicyAccepted = true;
            _emailService.Setup(e => e.IsValidEmailAddress(It.IsAny<System.Guid>(), It.IsAny<string>()))
                .Returns(GetServiceResponse_Result(It.IsAny<System.Guid>(), false));

            //Act
            var result = await _diagnosticToolControllerHelper.ProcessResults(form, FormTypes.DiagnosticTool);

            //Assert
            Assert.That(result.Payload, Is.False);
            Assert.That(form.validationErrors.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task ProcessResults_WithoutEmail_ShouldBeValid()
        {
            //Arrange
            var form = GetFormWithStep(8, out var errMsg, out var nextStep);

            _mapper.Setup(x => x.Map<DiagnosticToolEmailAnswerDto>(It.IsAny<EmailAnswer>())).Returns(new DiagnosticToolEmailAnswerDto());

            _emailService.Setup(e => e.SaveEmailAddress(It.IsAny<System.Guid>(), It.IsAny<DiagnosticToolEmailAnswerDto>()))
                .ReturnsAsync(GetServiceResponse_Result(It.IsAny<Guid>(), true, It.IsAny<string>(), It.IsAny<int>()));

            //Act
            var result = await _diagnosticToolControllerHelper.ProcessResults(form, FormTypes.DiagnosticTool);

            //Assert
            Assert.That(result.Payload, Is.True);
        }

        [Test]
        public async Task ProcessResults_SendResultsEmail_Success_Returns_True_Payload()
        {
            //Arrange
            var form = GetFormWithStep(8, out var errMsg, out var nextStep);

            _mapper.Setup(x => x.Map<DiagnosticToolEmailAnswerDto>(It.IsAny<EmailAnswer>())).Returns(new DiagnosticToolEmailAnswerDto());

            _emailService.Setup(e => e.SaveEmailAddress(It.IsAny<System.Guid>(), It.IsAny<DiagnosticToolEmailAnswerDto>()))
                .ReturnsAsync(GetServiceResponse_Result(It.IsAny<Guid>(), true, It.IsAny<string>(), It.IsAny<int>()));

            _emailService.Setup(e => e.SendResultsRemail(It.IsAny<System.Guid>(), It.IsAny<string>(), It.IsAny<IEmailDto>()))
                .ReturnsAsync(GetServiceResponse_Result(It.IsAny<Guid>(), true, It.IsAny<string>(), It.IsAny<int>()));

            //Act
            var result = await _diagnosticToolControllerHelper.ProcessResults(form, FormTypes.DiagnosticTool);

            //Assert
            Assert.That(result.Payload, Is.True);
        }

        [Test]
        public async Task ProcessResults_SendResultsEmail_Failure_Returns_False_Payload()
        {
            //Arrange
            var form = GetFormWithStep(8, out var errMsg, out var nextStep);

            _mapper.Setup(x => x.Map<DiagnosticToolEmailAnswerDto>(It.IsAny<EmailAnswer>())).Returns(new DiagnosticToolEmailAnswerDto());

            _emailService.Setup(e => e.SaveEmailAddress(It.IsAny<System.Guid>(), It.IsAny<DiagnosticToolEmailAnswerDto>()))
                .ReturnsAsync(GetServiceResponse_Result(It.IsAny<Guid>(), true, It.IsAny<string>(), It.IsAny<int>()));

            _emailService.Setup(e => e.SendResultsRemail(It.IsAny<System.Guid>(), It.IsAny<string>(), It.IsAny<IEmailDto>()))
                .ReturnsAsync(GetServiceResponse_Result(It.IsAny<Guid>(), false, It.IsAny<string>(), It.IsAny<int>()));

            //Act
            var result = await _diagnosticToolControllerHelper.ProcessResults(form, FormTypes.DiagnosticTool);

            //Assert
            Assert.That(result.Payload, Is.True);
        }


        [Test]
        public async Task ProcessResults_ValidEmailAndPrivacyPolicyAccepted_ShouldBeValid()
        {
            //Arrange

            //need to set this value, its set in program.cs for web project
            //or try mocking
            FormSearchTagsExtensions.StrapiApiUrl = "https://htg-dev-strapi-app.azurewebsites.net";

            var validEmail = "test@test.com";
            var form = GetFormWithStep(8, out var errMsg, out var nextStep);
            form.EmailAnswer.UserEmailAddress = validEmail;
            form.EmailAnswer.IsPrivacyPolicyAccepted = true;

            _mapper.Setup(x => x.Map<DiagnosticToolEmailAnswerDto>(It.IsAny<EmailAnswer>())).Returns(new DiagnosticToolEmailAnswerDto());

            _emailService.Setup(e => e.IsValidEmailAddress(It.IsAny<System.Guid>(), validEmail))
                .Returns(GetServiceResponse_Result(It.IsAny<System.Guid>(), true));

            _emailService.Setup(e => e.SaveEmailAddress(It.IsAny<System.Guid>(), It.IsAny<DiagnosticToolEmailAnswerDto>()))
                .ReturnsAsync(GetServiceResponse_Result(It.IsAny<Guid>(), true, It.IsAny<string>(), It.IsAny<int>()));

            _emailResponseHelperFactory.Setup(f => f.Get(It.IsAny<FormTypes>())).Returns(_emailResponseHelper.Object);
            _emailResponseHelper.Setup(h => h.ConvertToResultsEmail(It.IsAny<DiagnosticToolForm>())).ReturnsAsync(_emailDto.Object);

            _emailService.Setup(e => e.SendResultsRemail(It.IsAny<System.Guid>(), validEmail, It.IsAny<IEmailDto>()))
                .ReturnsAsync(GetServiceResponse_Result(It.IsAny<Guid>(), true, It.IsAny<string>()));

            //Act
            var result = await _diagnosticToolControllerHelper.ProcessResults(form, FormTypes.DiagnosticTool);

            //Assert
            Assert.That(result.Payload, Is.True);
        }

        [Test]
        public async Task CreateForm_Should_Populate_Content_Key()
        {
            //Act
            var result = await _diagnosticToolControllerHelper.CreateForm(FormTypes.DiagnosticTool);

            //Assert
            Assert.IsNotNull(result.Payload.ContentKey);
            Assert.AreEqual("diagnostic-tool-controller-helper-tests", result.Payload.ContentKey);
        }

        [TestCase("test-url", FormTypes.DiagnosticTool)]
        [TestCase("test-url", FormTypes.SkillsOne)]
        [TestCase("test-url", FormTypes.SkillsTwo)]
        [Test]
        public void GetUniqueContentKey_Populate_Content_Key_FormCompleted(string path, FormTypes formType)
        {
            _httpRequest.SetupGet(x => x.Path).Returns("/" + path);
            var form = GetForm(1, formType);
            form.FormIsCompleted = true;

            //Act
            var result = _diagnosticToolControllerHelper.GetUniqueContentKey(form);

            //Assert
            result.Should().Be(path + "-completed");
        }

        [TestCase("test-url", FormTypes.DiagnosticTool)]
        [TestCase("test-url", FormTypes.SkillsOne)]
        [TestCase("test-url", FormTypes.SkillsTwo)]
        [Test]
        public void GetUniqueContentKey_Populate_Content_Key_Form_Is_Not_Completed(string path, FormTypes formType)
        {
            _httpRequest.SetupGet(x => x.Path).Returns("/" + path);
            var form = GetForm(1, formType);
            form.FormIsCompleted = false;

            //Act
            var result = _diagnosticToolControllerHelper.GetUniqueContentKey(form);

            //Assert
            var currentStep = form.steps.SingleOrDefault(x => x.id == form.CurrStep);
            var contentKey = $"{path}-{currentStep.title}-{currentStep.id}";
            var expected = contentKey.Replace('/', '-').Replace(' ', '-').Trim(new char[] { ' ', '-' }).UrlEncode(true).ToLower();
            result.Should().Be(expected);
        }

        [Test]
        public void GetUniqueContentKey_Populate_Content_Key_Skilled_Three_FormCompleted()
        {
            var url = "newcomer-planning";
            var userTypeActionPlanSection = url;
            _httpRequest.SetupGet(x => x.Path).Returns("/test-url");
            var form = GetForm(1, FormTypes.SkillsThreeNewcomerPlanning);
            form.FormIsCompleted = true;
            form.userTypeActionPlanSection = userTypeActionPlanSection;

            //Act
            var result = _diagnosticToolControllerHelper.GetUniqueContentKey(form);

            //Assert
            result.Should().Be("test-url-newcomer-planning-completed");
        }

        [Test]
        public void GetUniqueContentKey_Populate_Content_Key_Skilled_Three_Form_Is_Not_Completed()
        {
            var url = "newcomer-planning";
            var userTypeActionPlanSection = url;
            _httpRequest.SetupGet(x => x.Path).Returns("/test-url");
            var form = GetForm(1, FormTypes.SkillsThreeNewcomerPlanning);
            form.FormIsCompleted = false;
            form.userTypeActionPlanSection = userTypeActionPlanSection;

            //Act
            var result = _diagnosticToolControllerHelper.GetUniqueContentKey(form);

            //Assert
            result.Should().Be("test-url-newcomer-planning-1");
        }

        private DiagnosticToolForm GetForm(int currentStep, FormTypes formtype)
        {
            var diagnosticToolFormService = new DiagnosticToolFormService(_loggerDiagnosticToolFormService.Object, _ctDisplayOptions, _applicationFormOptions);
            var diagnosticToolForm = diagnosticToolFormService.LoadNewForm(formtype);
            diagnosticToolForm.CurrStep = currentStep;
            return diagnosticToolForm;
        }

        [Test]
        public void LoadSkillsThreeResponseByUniqueId_WhenCalled_Success()
        {
            //Arrange 
            DiagnosticToolForm formInput = new DiagnosticToolForm
            {
                steps = {
                    new FormStep
                    {
                        id = 1,
                        elements = {
                            new FormStepElement
                            {
                                answerOptions =
                                {
                                    new FormAnswerOptionElement(),
                                    new FormAnswerOptionElement(),
                                    new FormAnswerOptionElement(),
                                }
                            }
                        }
                    },
                    new FormStep
                    {
                        id = 2,
                        elements = {
                            new FormStepElement
                            {
                                answerOptions =
                                {
                                    new FormAnswerOptionElement(),
                                    new FormAnswerOptionElement(),
                                    new FormAnswerOptionElement(),
                                }
                            }
                        }
                    },
                    new FormStep
                    {
                        id = 3,
                        elements = {
                            new FormStepElement
                            {
                                answerOptions =
                                {
                                    new FormAnswerOptionElement(),
                                    new FormAnswerOptionElement(),
                                    new FormAnswerOptionElement(),
                                }
                            }
                        }
                    },
                }
            };

            SkillsThreeResponse response = new SkillsThreeResponse
            {
                WhyNeedStart = "WhyNeedStart answer",
                WhyNeedNext = "WhyNeedNext answer",
                WhyNeedFinally = "WhyNeedFinally answer",
                HowAccessStart = "HowAccessStart answer",
                HowAccessNext = "HowAccessNext answer",
                HowAccessFinally = "HowAccessFinally answer",
                RiskStart = "RiskStart answer",
                RiskNext = "RiskStart answer",
                RiskFinally = "RiskStart answer"
            };

            string uniqueId = System.Guid.NewGuid().ToString();
            _skillsThreeService.Setup(x => x.FindByUniqueId(uniqueId)).Returns(response);
            //Act
            _diagnosticToolControllerHelper.LoadSkillsThreeResponseByUniqueId(uniqueId, formInput);

            //Assert
            Assert.AreEqual(formInput.steps[0].elements[0].answerOptions[0].value, response.WhyNeedStart);
            Assert.AreEqual(formInput.steps[0].elements[0].answerOptions[1].value, response.WhyNeedNext);
            Assert.AreEqual(formInput.steps[0].elements[0].answerOptions[2].value, response.WhyNeedFinally);

            Assert.AreEqual(formInput.steps[1].elements[0].answerOptions[0].value, response.HowAccessStart);
            Assert.AreEqual(formInput.steps[1].elements[0].answerOptions[1].value, response.HowAccessNext);
            Assert.AreEqual(formInput.steps[1].elements[0].answerOptions[2].value, response.HowAccessFinally);

            Assert.AreEqual(formInput.steps[2].elements[0].answerOptions[0].value, response.RiskStart);
            Assert.AreEqual(formInput.steps[2].elements[0].answerOptions[1].value, response.RiskNext);
            Assert.AreEqual(formInput.steps[2].elements[0].answerOptions[2].value, response.RiskFinally);
        }

        [Test]
        public async Task ProcessResults_SkillsOneForm_InvalidData()
        {
            var result = await _diagnosticToolControllerHelper.ProcessResults(new DiagnosticToolForm(), FormTypes.SkillsOne);

            result.Payload.Should().Be(false);
        }
        
        [Test]
        public async Task ProcessResults_SkillsTwoForm_InvalidData()
        {
            var result = await _diagnosticToolControllerHelper.ProcessResults(new DiagnosticToolForm(), FormTypes.SkillsTwo);

            result.Payload.Should().Be(false);
        }
        
        [Test]
        public async Task ProcessResults_SkillsThreeForm_InvalidData()
        {
            var result = await _diagnosticToolControllerHelper.ProcessResults(new DiagnosticToolForm(), FormTypes.SkillsThreeMoverCommunication);

            result.Payload.Should().Be(false);
        }
    }
}