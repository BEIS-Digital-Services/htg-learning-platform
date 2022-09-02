namespace Beis.LearningPlatform.Web.Controllers
{
    /// <summary>
    /// A class that defines a controller for the Diagnostic Tool.
    /// </summary>
    public class SkillsTwoController : FormControllerBase
    {
        private readonly IDiagnosticToolControllerHelper _controllerHelper;

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        public SkillsTwoController(ILogger<DiagnosticToolController> logger,
                                        IDiagnosticToolControllerHelper controllerHelper)
            : base(logger, controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        protected override string SessionEmailAnswer => "skills2_emailAnswer";
        protected override string SessionDiagnosticToolForm => "skills2_Form";

        [HttpPost]
        [Route("/skills-two/nextstep")]
        public async override Task<IActionResult> NextStep(DiagnosticToolForm model)
        {
            if (TryGetSessionData(out EmailAnswer emailAnswer))
                model.EmailAnswer = emailAnswer;

            var response = await _controllerHelper.NextStep(model, ModelState.IsValid, GetModelErrors());
            if (response.Result)
            {
                if (model.FormIsCompleted)
                {
                    //Get score
                    await _controllerHelper.UpdateScore(model);
                }

                return GetViewResult(response.Payload);
            }
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("/skills-two/gotostep")]
        public async override Task<IActionResult> GoToStep(DiagnosticToolForm model, [FromQuery] int step)
        {
            return await base.GoToStep(model, step);
        }

        [HttpPost]
        [Route("/skills-two/prevstep")]
        public async override Task<IActionResult> PrevStep(DiagnosticToolForm model)
        {
            return await base.PrevStep(model);
        }

        [HttpPost]
        [Route("/skills-two/result")]
        public async Task<IActionResult> Result(DiagnosticToolForm model)
        {
            var response = await _controllerHelper.ProcessResults(model, FormTypes.SkillsTwo);
            if (response.Result && response.Payload)
            {
                var isNewcomer = model.SkilledModuleTwoResultType == SkilledModuleTwoResultType.DigitalNewComer;
                return Redirect(isNewcomer ? "/going-digital-newcomer-next-steps" : "/learning-about-digital-adaption-next-steps");
            }
            else
            {
                return GetViewResult(model);
            }
        }

        [HttpGet]
        [Route("/skills-two")]
        [Route("/skills-two/start")]
        public async override Task<IActionResult> Start()
        {
            return await base.Start();
        }

        [HttpPost]
        [Route("/skills-two/startform")]
        public async override Task<IActionResult> StartForm(DiagnosticToolForm model)
        {
            return await base.StartForm(model);
        }

        [Route("/skills-two/summary")]
        public async  override Task<IActionResult> ChangeAnswers()
        {
            return await base.ChangeAnswers();
        }

        protected override FormTypes GetFormType()
        {
            return FormTypes.SkillsTwo;
        }

        [Route("/skills-two/startform")]
        public async override Task<IActionResult> StartForm()
        {
            return await Start();
        }
    }
}