namespace Beis.LearningPlatform.Web.Controllers
{
    /// <summary>
    /// A class that defines a controller for the Diagnostic Tool.
    /// </summary>
    public class SkillsOneController : FormControllerBase
    {
        private readonly IDiagnosticToolControllerHelper _controllerHelper;

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        public SkillsOneController(ILogger<DiagnosticToolController> logger,
                                        IDiagnosticToolControllerHelper controllerHelper,
                                        ISessionService sessionService)
            : base(logger, controllerHelper, sessionService)
        {
            _controllerHelper = controllerHelper;
        }

        protected override string SessionEmailAnswer => "skills1_emailAnswer";
        protected override string SessionDiagnosticToolForm => "skills1_Form";

        [HttpPost]
        [Route("/skills-one/nextstep")]
        public async override Task<IActionResult> NextStep(DiagnosticToolForm model)
        {
            return await base.NextStep(model);
        }

        [HttpPost]
        [Route("/skills-one/gotostep")]
        public async override Task<IActionResult> GoToStep(DiagnosticToolForm model, [FromQuery] int step)
        {
            return await base.GoToStep(model, step);
        }

        [HttpPost]
        [Route("/skills-one/prevstep")]
        public async override Task<IActionResult> PrevStep(DiagnosticToolForm model)
        {
            return await base.PrevStep(model);
        }

        [HttpPost]
        [Route("/skills-one/result")]
        public async Task<IActionResult> Result(DiagnosticToolForm model)
        {
            var response = await _controllerHelper.ProcessResults(model, FormTypes.SkillsOne);
            if (response.Result && response.Payload)
            {
                return Redirect("/learning-module-one-next-steps");
            }
            else
            {
                return GetViewResult(model);
            }
        }

        [HttpGet]
        [Route("/skills-one")]
        [Route("/skills-one/start")]
        public async override Task<IActionResult> Start()
        {
            return await base.Start();
        }

        [HttpPost]
        [Route("/skills-one/startform")]
        public async override Task<IActionResult> StartForm(DiagnosticToolForm model)
        {
            return await base.StartForm(model);
        }

        [Route("/skills-one/summary")]
        public async override Task<IActionResult> ChangeAnswers()
        {
            return await base.ChangeAnswers();
        }

        protected override FormTypes GetFormType()
        {
            return FormTypes.SkillsOne;
        }


        [Route("/skills-one/startform")]
        public async override Task<IActionResult> StartForm()
        {
            return await Start();
        }
    }
}