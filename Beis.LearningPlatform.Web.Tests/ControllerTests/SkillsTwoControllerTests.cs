using Beis.HelpToGrow.Common.Interfaces;

namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class SkillsTwoControllerTests : FormControllerBaseTest
    {
        protected override FormTypes GetFormType()
        {
            return FormTypes.SkillsTwo;
        }

        private Mock<ILogger<DiagnosticToolController>> _logger;
        private Mock<ISessionService> _sessionService;
        private SkillsTwoController controller;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<DiagnosticToolController>>();
            _diagnosticToolControllerHelper = new();
            _ctDisplayOptions.Value.LoadFormFromJson = true;
            _sessionService = new Mock<ISessionService>();

            controller = new SkillsTwoController(_logger.Object, _diagnosticToolControllerHelper.Object, _sessionService.Object);
            SetHttpContext(controller.ControllerContext);
        }

        [Test]
        public async Task Start_WhenCalled_ReturnsOK()
        {

            //Arrange
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
        public async Task NextStep_Get_WhenCalled_ReturnsOK()
        {
            //Arrange
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.NextStep();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task PrevStep_Get_WhenCalled_ReturnsOK()
        {
            //Arrange
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.PrevStep();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task StartForm_Get_WhenCalled_ReturnsOK()
        {
            //Arrange
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.StartForm();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Result_Get_WhenCalled_ReturnsOK()
        {
            //Arrange
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.Result();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }


        [Test]
        public async Task Result_Get_Form_WhenCalled_ReturnsOK()
        {
            //Arrange
            Setup_ControllerHelperCreateForm();
            _diagnosticToolControllerHelper.Setup(h => h.ProcessResults(It.IsAny<DiagnosticToolForm>(), FormTypes.SkillsTwo))
                .ReturnsAsync(new ControllerHelperOperationResponse<bool>(Guid.Empty, true));
            var form = Get_DiagnosticToolForm(1);

            //Act
            var result = await controller.Result(form);

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}