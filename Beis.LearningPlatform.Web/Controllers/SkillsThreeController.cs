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
        //[Route("/skills-three")]
        [Route("/skills-three-newcomer-planning")]
        [Route("/skills-three-newcomer-communication")]
        //[Route("/skills-three-newcomer-planning/start")]
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