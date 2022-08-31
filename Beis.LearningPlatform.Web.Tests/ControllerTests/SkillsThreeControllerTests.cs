namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class SkillsThreeControllerTests : FormControllerBaseTest
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new();
        private readonly Mock<HttpContext> _httpContext = new();
        private readonly Mock<HttpRequest> _httpRequest = new();
        private Mock<ISession> _session = new();
        private string _currentPath;
        protected override FormTypes GetFormType()
        {
            var formType = FormTypes.SkillsThreeNewcomerPlanning;
            switch (_currentPath)
            {
                case "skills-three-newcomer-planning":
                    formType = FormTypes.SkillsThreeNewcomerPlanning;
                    break;
                case "skills-three-newcomer-communication":
                    formType = FormTypes.SkillsThreeNewcomerCommunication;
                    break;
                case "skills-three-newcomer-support":
                    formType = FormTypes.SkillsThreeNewcomerSupport;
                    break;
                case "skills-three-newcomer-training":
                    formType = FormTypes.SkillsThreeNewcomerTraining;
                    break;
                case "skills-three-newcomer-testing":
                    formType = FormTypes.SkillsThreeNewcomerTesting;
                    break;

                case "skills-three-mover-planning":
                    formType = FormTypes.SkillsThreeMoverPlanning;
                    break;
                case "skills-three-mover-communication":
                    formType = FormTypes.SkillsThreeMoverCommunication;
                    break;
                case "skills-three-mover-support":
                    formType = FormTypes.SkillsThreeMoverSupport;
                    break;
                case "skills-three-mover-training":
                    formType = FormTypes.SkillsThreeMoverTraining;
                    break;
                case "skills-three-mover-testing":
                    formType = FormTypes.SkillsThreeMoverTesting;
                    break;


                case "skills-three-performer-planning":
                    formType = FormTypes.SkillsThreePerformerPlanning;
                    break;
                case "skills-three-performer-communication":
                    formType = FormTypes.SkillsThreePerformerCommunication;
                    break;
                case "skills-three-performer-support":
                    formType = FormTypes.SkillsThreePerformerSupport;
                    break;
                case "skills-three-performer-training":
                    formType = FormTypes.SkillsThreePerformerTraining;
                    break;
                case "skills-three-performer-testing":
                    formType = FormTypes.SkillsThreePerformerTesting;
                    break;

            }

            return formType;
        }

        private Mock<ILogger<DiagnosticToolController>> _logger;
        private SkillsThreeController controller;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<DiagnosticToolController>>();
            _diagnosticToolControllerHelper = new();
            _ctDisplayOptions.Value.LoadFormFromJson = true;
            _currentPath = string.Empty;

            controller = new SkillsThreeController(_logger.Object, _diagnosticToolControllerHelper.Object, _httpContextAccessor.Object);
            SetHttpContext(controller.ControllerContext);

            _httpContext.SetupGet(x => x.Request)
                .Returns(_httpRequest.Object);
            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);
            _httpContext.SetupGet(x => x.Session).Returns(_session.Object);
        }

        [TestCase("skills-three-newcomer-planning")]
        [TestCase("skills-three-newcomer-communication")]
        [TestCase("skills-three-newcomer-support")]
        [TestCase("skills-three-newcomer-training")]
        [TestCase("skills-three-newcomer-testing")]

        [TestCase("skills-three-mover-planning")]
        [TestCase("skills-three-mover-communication")]
        [TestCase("skills-three-mover-support")]
        [TestCase("skills-three-mover-training")]
        [TestCase("skills-three-mover-testing")]

        [TestCase("skills-three-performer-planning")]
        [TestCase("skills-three-performer-communication")]
        [TestCase("skills-three-performer-support")]
        [TestCase("skills-three-performer-training")]
        [TestCase("skills-three-performer-testing")]
        public async Task Start_WhenCalled_ReturnsOK(string path)
        {

            //Arrange
            _currentPath = path;
            _httpRequest.SetupGet(x => x.Path).Returns("/" + path);
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.Start();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task StartForm_Post_WhenCalled_ReturnsOK()
        {
            //Arrange
            _diagnosticToolControllerHelper.Setup(h => h.Start(It.IsAny<DiagnosticToolForm>()))
                .ReturnsAsync(new ControllerHelperOperationResponse(It.IsAny<Guid>(), true));

            //Act
            var result = await controller.StartForm(Get_DiagnosticToolForm(1));

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task NextStep_Post_WhenCalled_ReturnsOK()
        {
            //Arrange
            _diagnosticToolControllerHelper.Setup(h => h.NextStep(It.IsAny<DiagnosticToolForm>(), true, It.IsAny<string[]>()))
                .ReturnsAsync(new ControllerHelperOperationResponse<DiagnosticToolForm>(It.IsAny<Guid>(), true, Get_DiagnosticToolForm(2)));

            //Act
            var result = await controller.NextStep(Get_DiagnosticToolForm(1));

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task PrevStep_Post_WhenCalled_ReturnsOK()
        {
            //Arrange
            _diagnosticToolControllerHelper.Setup(h => h.PreviousStep(It.IsAny<DiagnosticToolForm>()))
                .ReturnsAsync(new ControllerHelperOperationResponse<DiagnosticToolForm>(It.IsAny<Guid>(), true, Get_DiagnosticToolForm(1)));

            //Act
            var result = await controller.PrevStep(Get_DiagnosticToolForm(2));

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task GoToStep_WhenCalled_ReturnsOK()
        {
            //Arrange
            var goToStep = 3;
            _diagnosticToolControllerHelper.Setup(h => h.GotoStep(It.IsAny<DiagnosticToolForm>(), goToStep))
                .ReturnsAsync(new ControllerHelperOperationResponse<DiagnosticToolForm>(It.IsAny<Guid>(), true, Get_DiagnosticToolForm(goToStep)));

            //Act
            var result = await controller.GoToStep(Get_DiagnosticToolForm(2), goToStep);

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Result_Post_WhenCalled_ReturnsOK()
        {
            //Arrange
            _diagnosticToolControllerHelper.Setup(h => h.ProcessResults(It.IsAny<DiagnosticToolForm>(), It.IsAny<FormTypes>()))
                .ReturnsAsync(new ControllerHelperOperationResponse<bool>(It.IsAny<Guid>(), true, true));


            //Act
            var result = await controller.Result(Get_DiagnosticToolForm(1));

            //Assert
            Assert.NotNull(result);
        }


        [TestCase("skills-three-newcomer-planning")]
        [TestCase("skills-three-newcomer-communication")]
        [TestCase("skills-three-newcomer-support")]
        [TestCase("skills-three-newcomer-training")]
        [TestCase("skills-three-newcomer-testing")]

        [TestCase("skills-three-mover-planning")]
        [TestCase("skills-three-mover-communication")]
        [TestCase("skills-three-mover-support")]
        [TestCase("skills-three-mover-training")]
        [TestCase("skills-three-mover-testing")]

        [TestCase("skills-three-performer-planning")]
        [TestCase("skills-three-performer-communication")]
        [TestCase("skills-three-performer-support")]
        [TestCase("skills-three-performer-training")]
        [TestCase("skills-three-performer-testing")]
        public async Task NextStep_Get_WhenCalled_ReturnsOK(string path)
        {
            //Arrange
            _currentPath = path;
            _httpRequest.SetupGet(x => x.Path).Returns("/" + path);

            Setup_ControllerHelperCreateForm();
            
            //Act
            var result = await controller.NextStep();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [TestCase("skills-three-newcomer-planning")]
        [TestCase("skills-three-newcomer-communication")]
        [TestCase("skills-three-newcomer-support")]
        [TestCase("skills-three-newcomer-training")]
        [TestCase("skills-three-newcomer-testing")]

        [TestCase("skills-three-mover-planning")]
        [TestCase("skills-three-mover-communication")]
        [TestCase("skills-three-mover-support")]
        [TestCase("skills-three-mover-training")]
        [TestCase("skills-three-mover-testing")]

        [TestCase("skills-three-performer-planning")]
        [TestCase("skills-three-performer-communication")]
        [TestCase("skills-three-performer-support")]
        [TestCase("skills-three-performer-training")]
        [TestCase("skills-three-performer-testing")]
        public async Task PrevStep_Get_WhenCalled_ReturnsOK(string path)
        {
            //Arrange
            _currentPath = path;
            _httpRequest.SetupGet(x => x.Path).Returns("/" + path);
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.PrevStep();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [TestCase("skills-three-newcomer-planning")]
        [TestCase("skills-three-newcomer-communication")]
        [TestCase("skills-three-newcomer-support")]
        [TestCase("skills-three-newcomer-training")]
        [TestCase("skills-three-newcomer-testing")]

        [TestCase("skills-three-mover-planning")]
        [TestCase("skills-three-mover-communication")]
        [TestCase("skills-three-mover-support")]
        [TestCase("skills-three-mover-training")]
        [TestCase("skills-three-mover-testing")]

        [TestCase("skills-three-performer-planning")]
        [TestCase("skills-three-performer-communication")]
        [TestCase("skills-three-performer-support")]
        [TestCase("skills-three-performer-training")]
        [TestCase("skills-three-performer-testing")]
        public async Task StartForm_Get_WhenCalled_ReturnsOK(string path)
        {
            //Arrange
            _currentPath = path;
            _httpRequest.SetupGet(x => x.Path).Returns("/" + path);
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.StartForm();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [TestCase("skills-three-newcomer-planning")]
        [TestCase("skills-three-newcomer-communication")]
        [TestCase("skills-three-newcomer-support")]
        [TestCase("skills-three-newcomer-training")]
        [TestCase("skills-three-newcomer-testing")]

        [TestCase("skills-three-mover-planning")]
        [TestCase("skills-three-mover-communication")]
        [TestCase("skills-three-mover-support")]
        [TestCase("skills-three-mover-training")]
        [TestCase("skills-three-mover-testing")]

        [TestCase("skills-three-performer-planning")]
        [TestCase("skills-three-performer-communication")]
        [TestCase("skills-three-performer-support")]
        [TestCase("skills-three-performer-training")]
        [TestCase("skills-three-performer-testing")]
        public async Task Result_Get_WhenCalled_ReturnsOK(string path)
        {
            //Arrange
            _currentPath = path;
            _httpRequest.SetupGet(x => x.Path).Returns("/" + path);
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.Result();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [TestCase("skills-three-newcomer-planning", "/your-digital-newcomer-action-plan")]
        [TestCase("skills-three-newcomer-communication", "/your-digital-newcomer-action-plan")]
        [TestCase("skills-three-newcomer-support", "/your-digital-newcomer-action-plan")]
        [TestCase("skills-three-newcomer-training", "/your-digital-newcomer-action-plan")]
        [TestCase("skills-three-newcomer-testing", "/your-digital-newcomer-action-plan")]

        [TestCase("skills-three-mover-planning", "/your-digital-mover-action-plan")]
        [TestCase("skills-three-mover-communication", "/your-digital-mover-action-plan")]
        [TestCase("skills-three-mover-support", "/your-digital-mover-action-plan")]
        [TestCase("skills-three-mover-training", "/your-digital-mover-action-plan")]
        [TestCase("skills-three-mover-testing", "/your-digital-mover-action-plan")]

        [TestCase("skills-three-performer-planning", "/your-digital-performer-action-plan")]
        [TestCase("skills-three-performer-communication", "/your-digital-performer-action-plan")]
        [TestCase("skills-three-performer-support", "/your-digital-performer-action-plan")]
        [TestCase("skills-three-performer-training", "/your-digital-performer-action-plan")]
        [TestCase("skills-three-performer-testing", "/your-digital-performer-action-plan")]
        public async Task Result_backURLfromQ1_Correctly_Configured(string path, string expectedBackURLfromQ1)
        {
            //Arrange
            _currentPath = path;
            _httpRequest.SetupGet(x => x.Path).Returns("/" + path);
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.Result();

            //Assert
            Assert.NotNull(result);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);

            var diagnosticToolForm = viewResult.Model as DiagnosticToolForm;
            Assert.IsNotNull(diagnosticToolForm);

            Assert.IsNotNull(diagnosticToolForm.backURLfromQ1);
            Assert.AreEqual(expectedBackURLfromQ1, diagnosticToolForm.backURLfromQ1);
            
        }
    }
}
