using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Models.DiagnosticTool;
using Beis.LearningPlatform.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Controllers
{
    /// <summary>
    /// A class that defines a controller for the Diagnostic Tool.
    /// </summary>
    public class SkillsThreeController : FormControllerBase
    {
        private readonly IDiagnosticToolControllerHelper _controllerHelper;

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        public SkillsThreeController(ILogger<DiagnosticToolController> logger,
                                        IDiagnosticToolControllerHelper controllerHelper)
            : base(logger, controllerHelper)
        {
            _controllerHelper = controllerHelper;
        }

        protected override string SessionEmailAnswer => "skills3_emailAnswer";
        protected override string SessionDiagnosticToolForm => "skills3_Form";

        [HttpPost]
        //Tas1[Route("/skills-three-newcomer-planning/nextstep")]
        //Tas1[Route("/skills-three-newcomer-communication/nextstep")]
        public async override Task<IActionResult> NextStep(DiagnosticToolForm model)
        {
            return await base.NextStep(model);
        }

        //[HttpPost]
        //[Route("/skills-three-newcomer-planning/gotostep")]
        //public async override Task<IActionResult> GoToStep(DiagnosticToolForm model, [FromQuery] int step)
        //{
        //    return await base.GoToStep(model, step);
        //}

        [HttpPost]
        //[Route("/skills-three-newcomer-planning/prevstep")]
        //[Route("/skills-three-newcomer-communication/prevstep")]
        public async override Task<IActionResult> PrevStep(DiagnosticToolForm model)
        {
            return await base.PrevStep(model);
        }

        [HttpPost]
        [Route("/skills-three-newcomer-planning/result")]
        [Route("/skills-three-newcomer-communication/result")]
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

        //[HttpPost]
        //[Route("/skills-three-newcomer-planning/startform")]
        //public async override Task<IActionResult> StartForm(DiagnosticToolForm model)
        //{
        //    return await base.StartForm(model);
        //}

        //[Route("/skills-three-newcomer-planning/summary")]
        //[Route("/skills-three-newcomer-communication/summary")]
        //public async override Task<IActionResult> ChangeAnswers()
        //{
        //    return await base.ChangeAnswers();
        //}

        protected override FormTypes GetFormType()
        {
            var route = Request.Path.Value.ToLower().Replace("/", "").Trim();
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


        //[Route("/skills-three-newcomer-planning/startform")]
        //public async override Task<IActionResult> StartForm()
        //{
        //    return await Start();
        //}
    }
}