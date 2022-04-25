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
    public class SkillsTwoController : ControllerBase
    {
        private const string cSESSION_EMAIL_ANSWER = "skills2_emailAnswer";
        private const string cSESSION_DIAGNOSTIC_TOOL_FORM = "skills2_Form";
        private readonly IDiagnosticToolControllerHelper _controllerHelper;


        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        public SkillsTwoController(ILogger<DiagnosticToolController> logger,
                                        IDiagnosticToolControllerHelper controllerHelper)
            : base(logger)
        {
            _controllerHelper = controllerHelper;
        }

        private string[] GetModelErrors()
        {
            return ModelState.Values
                             .Where(x => x.Errors?.Count > 0)
                             .SelectMany(x => x.Errors)
                             .Select(x => x.ErrorMessage)
                             .ToArray();
        }

        private void ClearSessionEmail()
        {
            HttpContext.Session.ClearSessionData(cSESSION_EMAIL_ANSWER);
        }

        private void ClearSessionForm()
        {
            HttpContext.Session.ClearSessionData(cSESSION_DIAGNOSTIC_TOOL_FORM);
        }

        private ViewResult GetViewResult(DiagnosticToolForm model, bool showResults = false)
        {
            string viewName = showResults ? "Result" : "Start";
            model.IsInstructionsPage = model.CurrStep == 0;
            if (showResults)
            {
                model.pageTitle = "Help to Grow: Digital - Diagnostic Tool - Results";
            }
            return View(viewName, model);
        }

        private void SetSessionEmail(EmailAnswer emailAnswer)
        {
            HttpContext.Session.SetSessionData(cSESSION_EMAIL_ANSWER, emailAnswer);
        }

        private bool TryGetSessionData(out EmailAnswer emailAnswer)
        {
            return HttpContext.Session.TryGetSessionData(cSESSION_EMAIL_ANSWER, out emailAnswer);
        }

        private async Task<DiagnosticToolForm> TryGetSessionForm()
        {
            DiagnosticToolForm returnValue = default;

            if (HttpContext.Session.TryGetSessionData(cSESSION_DIAGNOSTIC_TOOL_FORM, out FormStepAnswer[] answers))
            {
                var createFormResponse = await _controllerHelper.CreateForm(FormTypes.SkillsTwo);
                if (createFormResponse.Result)
                {
                    returnValue = createFormResponse.Payload;
                    returnValue.Load(answers);
                }
            }

            return returnValue;
        }

        [HttpPost]
        [Route("/skills-two/gotostep")]
        public async Task<IActionResult> GoToStep(DiagnosticToolForm model, [FromQuery] int step)
        {
            SetSessionEmail(model.EmailAnswer);
            var response = await _controllerHelper.GotoStep(model, step);
            if (response.Result)
                return GetViewResult(response.Payload);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("/skills-two/nextstep")]
        public async Task<IActionResult> NextStep(DiagnosticToolForm model)
        {
            if (TryGetSessionData(out EmailAnswer emailAnswer))
                model.EmailAnswer = emailAnswer;

            var response = await _controllerHelper.NextStep(model, ModelState.IsValid, GetModelErrors());
            if (response.Result)
            {
                if(model.FormIsCompleted)
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
        [Route("/skills-two/prevstep")]
        public async Task<IActionResult> PrevStep(DiagnosticToolForm model)
        {
            var response = await _controllerHelper.PreviousStep(model);
            if (response.Result)
                return GetViewResult(response.Payload);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("/skills-two/result")]
        public async Task<IActionResult> Result(DiagnosticToolForm model)
        {
            var response = await _controllerHelper.ProcessResults(model, FormTypes.SkillsTwo);
            if (response.Result && response.Payload)
            {
                return Redirect("/learning-module-two-next-steps");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/skills-two")]
        [Route("/skills-two/start")]
        public async Task<IActionResult> Start()
        {
            ClearSessionForm();
            ClearSessionEmail();
            var createFormResponse = await _controllerHelper.CreateForm(FormTypes.SkillsTwo);
            if (createFormResponse.Result)
            {
                var startResponse = await _controllerHelper.Start(createFormResponse.Payload);
                if (startResponse.Result)
                {
                    return GetViewResult(createFormResponse.Payload);
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("/skills-two/startform")]
        public async Task<IActionResult> StartForm(DiagnosticToolForm model)
        {
            ClearSessionForm();
            var response = await _controllerHelper.Start(model);
            if (response.Result)
                return GetViewResult(model);
            else
                return BadRequest();
        }

        [Route("/skills-two/summary")]
        public async Task<IActionResult> ChangeAnswers()
        {
            var model = await TryGetSessionForm();
            if (model != default)
            {
                model.CurrStep = model.steps.Count + 1;
                return await NextStep(model);
            }
            else
                return await Start();
        }

        #region Used by Cookie Acceptance Redirection
        //TODO: LP-967 Cookies Cannot Be Accepted or Rejected in DT
        // ******************************************************************************************************************************************
        // Currently, the Cookie acceptance uses an href to process the accepted cookie. As the Diagnostic Tool uses Form Post, the view model is not transferred
        // after the cookie processing occurs. These actions below are used to redirect the Diagnostic Tool to the Start page
        // ******************************************************************************************************************************************

        // IMPORTANT User will have to return to the beginning of the diagnostic tool if a direct route to any step other than the beginning is used
        // This is essential because the tool populates and maintains the data in the model between server calls
        public async Task<IActionResult> NextStep()
        {
            return await Start();
        }

        // IMPORTANT User will have to return to the beginning of the diagnostic tool if a direct route to any step other than the beginning is used
        // This is essential because the tool populates and maintains the data in the model between server calls
        public async Task<IActionResult> PrevStep()
        {
            return await Start();
        }

        // IMPORTANT User will have to return to the beginning of the diagnostic tool if a direct route to any step other than the beginning is used
        // This is essential because the tool populates and maintains the data in the model between server calls
        public async Task<IActionResult> Result()
        {
            return await Start();
        }

        // IMPORTANT User will have to return to the beginning of the diagnostic tool if a direct route to any step other than the beginning is used
        // This is essential because the tool populates and maintains the data in the model between server calls
        [Route("/skills-two/startform")]
        public async Task<IActionResult> StartForm()
        {
            return await Start();
        }
        #endregion
    }
}