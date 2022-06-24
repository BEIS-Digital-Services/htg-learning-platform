namespace Beis.LearningPlatform.Web.Controllers
{
    /// <summary>
    /// A class that defines a controller for the Diagnostic Tool.
    /// </summary>
    public class SkillsThreeController : FormControllerBase
    {
        private readonly IDiagnosticToolControllerHelper _controllerHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        public SkillsThreeController(ILogger<DiagnosticToolController> logger,
                                        IDiagnosticToolControllerHelper controllerHelper,
                                        IHttpContextAccessor httpContextAccessor)
            : base(logger, controllerHelper)
        {
            _controllerHelper = controllerHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override string SessionEmailAnswer => "skills3_emailAnswer";
        protected override string SessionDiagnosticToolForm => "skills3_Form";

        [HttpPost]
        public async override Task<IActionResult> NextStep(DiagnosticToolForm model)
        {
            return await base.NextStep(model);
        }


        [HttpPost]
        public async override Task<IActionResult> PrevStep(DiagnosticToolForm model)
        {
            return await base.PrevStep(model);
        }

        [HttpPost]
        [Route("/skills-three-newcomer-planning/result")]
        [Route("/skills-three-newcomer-communication/result")]
        public async Task<IActionResult> Result(DiagnosticToolForm model)
        {
            var response = await _controllerHelper.ProcessResults(model, GetFormType());
            if (response.Result && response.Payload)
            {
                string completedSessionKey = $"{model.userTypeActionPlanSection}__CompletedLink";
                _httpContextAccessor.HttpContext.Session.SetString(completedSessionKey, "true");
                return Redirect($"/learning-completed-{model.GetFormUrlName()}");
            }
            else
            {
                return GetViewResult(model);
            }
        }

        [HttpGet]
        
        [Route("/skills-three-newcomer-planning")]
        [Route("/skills-three-newcomer-communication")]
        [Route("/skills-three-newcomer-support")]
        [Route("/skills-three-newcomer-training")]
        [Route("/skills-three-newcomer-testing")]

        [Route("/skills-three-mover-planning")]
        [Route("/skills-three-mover-communication")]
        [Route("/skills-three-mover-support")]
        [Route("/skills-three-mover-training")]
        [Route("/skills-three-mover-testing")]

        [Route("/skills-three-performer-planning")]
        [Route("/skills-three-performer-communication")]
        [Route("/skills-three-performer-support")]
        [Route("/skills-three-performer-training")]
        [Route("/skills-three-performer-testing")]

        public async override Task<IActionResult> Start()
        {
            //var route = Request.Path.Value;
            return await base.Start();
        }


        protected override FormTypes GetFormType()
        {
            var route = _httpContextAccessor.HttpContext.Request.Path.Value?.ToLower().Replace("/", "").Trim();
            var formType = FormTypes.SkillsThreeNewcomerPlanning;
            switch (route)
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
    }
}