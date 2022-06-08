namespace Beis.LearningPlatform.Web.Controllers
{
    /// <summary>
    /// A class that defines the base functionality of a controller.
    /// </summary>
    public class FormControllerBase : ControllerBase
    {
        private readonly IDiagnosticToolControllerHelper _controllerHelper;

        public FormControllerBase(ILogger<ControllerBase> logger,
            IDiagnosticToolControllerHelper controllerHelper) : base(logger)
        {
            _controllerHelper = controllerHelper;
        }

        protected virtual string SessionEmailAnswer => "emailAnswer";
        protected virtual string SessionDiagnosticToolForm => "diagnosticToolForm";

        protected string[] GetModelErrors()
        {
            return ModelState.Values
                             .Where(x => x.Errors?.Count > 0)
                             .SelectMany(x => x.Errors)
                             .Select(x => x.ErrorMessage)
                             .ToArray();
        }

        protected void ClearSessionEmail()
        {
            HttpContext.Session.ClearSessionData(SessionEmailAnswer);
        }

        protected void ClearSessionForm()
        {
            HttpContext.Session.ClearSessionData(SessionDiagnosticToolForm);
        }

        protected ViewResult GetViewResult(DiagnosticToolForm model, bool showResults = false)
        {
            string viewName = showResults ? "Result" : "Start";
            model.IsInstructionsPage = model.CurrStep == 0;
            if (showResults)
            {
                model.pageTitle = "Help to Grow: Digital - Diagnostic Tool - Results";
            }
            return View(viewName, model);
        }

        protected void SetSessionEmail(EmailAnswer emailAnswer)
        {
            HttpContext.Session.SetSessionData(SessionEmailAnswer, emailAnswer);
        }

        protected void SetSessionForm(DiagnosticToolForm diagnosticToolForm)
        {
            var answerData = diagnosticToolForm.Save();
            HttpContext.Session.SetSessionData(SessionDiagnosticToolForm, answerData);
        }

        protected bool TryGetSessionData(out EmailAnswer emailAnswer)
        {
            return HttpContext.Session.TryGetSessionData(SessionEmailAnswer, out emailAnswer);
        }

        protected async Task<DiagnosticToolForm> TryGetSessionForm()
        {
            DiagnosticToolForm returnValue = default;

            if (HttpContext.Session.TryGetSessionData(SessionDiagnosticToolForm, out FormStepAnswer[] answers))
            {
                var createFormResponse = await _controllerHelper.CreateForm(GetFormType());
                if (createFormResponse.Result)
                {
                    returnValue = createFormResponse.Payload;
                    returnValue.Load(answers);
                }
            }

            return returnValue;
        }


        public async virtual Task<IActionResult> GoToStep(DiagnosticToolForm model, [FromQuery] int step)
        {
            SetSessionEmail(model.EmailAnswer);
            var response = await _controllerHelper.GotoStep(model, step);
            if (response.Result)
                return GetViewResult(response.Payload);
            else
                return BadRequest();
        }


        public async virtual Task<IActionResult> NextStep(DiagnosticToolForm model)
        {
            if (TryGetSessionData(out EmailAnswer emailAnswer))
                model.EmailAnswer = emailAnswer;

            var response = await _controllerHelper.NextStep(model, ModelState.IsValid, GetModelErrors());
            if (response.Result)
                return GetViewResult(response.Payload);
            else
                return BadRequest();
        }

        public async virtual Task<IActionResult> PrevStep(DiagnosticToolForm model)
        {
            var response = await _controllerHelper.PreviousStep(model);
            if (response.Result)
                return GetViewResult(response.Payload);
            else
                return BadRequest();
        }

        public async virtual Task<IActionResult> StartForm(DiagnosticToolForm model)
        {
            ClearSessionForm();
            var response = await _controllerHelper.Start(model);
            if (response.Result)
                return GetViewResult(model);
            else
                return BadRequest();
        }

        public async virtual Task<IActionResult> Start()
        {
            ClearSessionForm();
            ClearSessionEmail();
            var createFormResponse = await _controllerHelper.CreateForm(GetFormType());
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

        public async virtual Task<IActionResult> ChangeAnswers()
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

        protected virtual FormTypes GetFormType()
        {
            return FormTypes.DiagnosticTool;
        }

        // Used by Cookie Acceptance Redirection
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
        public async virtual Task<IActionResult> StartForm()
        {
            return await Start();
        }

        
    }
}
