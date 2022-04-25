using Beis.LearningPlatform.Web.ComparisonTool.Models;
using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models.DiagnosticTool;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Controllers
{
    /// <summary>
    /// A class that defines a controller for the Diagnostic Tool.
    /// </summary>
    public class DiagnosticToolController : ControllerBase
    {
        private const int CacheDurationInSeconds = 120;
        private const string cSESSION_EMAIL_ANSWER = "emailAnswer";
        private const string cSESSION_DIAGNOSTIC_TOOL_FORM = "diagnosticToolForm";

        private readonly IDiagnosticToolControllerHelper _controllerHelper;
        private readonly IComparisonToolService _comparisonToolService;
        private readonly List<CMSSearchTag> _productCategories;
        private readonly ComparisonToolDisplayOption _ctDisplayOption;

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        public DiagnosticToolController(ILogger<DiagnosticToolController> logger,
                                        IDiagnosticToolControllerHelper controllerHelper,
                                        IComparisonToolService comparisonToolService,
                                        IOptions<ComparisonToolDisplayOption> ctDisplayOption)
            : base(logger)
        {
            _controllerHelper = controllerHelper;
            _comparisonToolService = comparisonToolService;
            _ctDisplayOption = ctDisplayOption.Value;

            _productCategories = new List<CMSSearchTag>
            {
                new() { id = 1, name = "accounting", displayName = "DIGITAL ACCOUNTING SOFTWARE" },
                new() { id = 2, name = "crm", displayName = "CUSTOMER RELATIONSHIP MANAGEMENT (CRM) SOFTWARE" }
            };

            if (_ctDisplayOption.ShowECommerce ?? false)
            {
                _productCategories.Add(new CMSSearchTag { id = 3, name = "ecommerce", displayName = "ECOMMERCE" });
            }
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

        private void SetSessionForm(DiagnosticToolForm diagnosticToolForm)
        {
            var answerData = diagnosticToolForm.Save();
            HttpContext.Session.SetSessionData(cSESSION_DIAGNOSTIC_TOOL_FORM, answerData);
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
                var createFormResponse = await _controllerHelper.CreateForm(FormTypes.DiagnosticTool);
                if (createFormResponse.Result)
                {
                    returnValue = createFormResponse.Payload;
                    returnValue.Load(answers);
                }
            }

            return returnValue;
        }

        [HttpPost]
        [Route("/diagnostic-tool/gotostep")]
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
        [Route("/diagnostic-tool/nextstep")]
        public async Task<IActionResult> NextStep(DiagnosticToolForm model)
        {
            if (TryGetSessionData(out EmailAnswer emailAnswer))
                model.EmailAnswer = emailAnswer;

            var response = await _controllerHelper.NextStep(model, ModelState.IsValid, GetModelErrors());
            if (response.Result)
                return GetViewResult(response.Payload);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("/diagnostic-tool/prevstep")]
        public async Task<IActionResult> PrevStep(DiagnosticToolForm model)
        {
            var response = await _controllerHelper.PreviousStep(model);
            if (response.Result)
                return GetViewResult(response.Payload);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("/diagnostic-tool/result")]
        public async Task<IActionResult> Result(DiagnosticToolForm model)
        {
            var response = await _controllerHelper.ProcessResults(model, FormTypes.DiagnosticTool);
            model.ProductCategories = _productCategories;
            
            if (response.Result)
            {
                // Only process the products if the result was completed successfully
                if (response.Payload)
                {
                    // Collate user interest from Question 7 or 8, depending on answer to question 6
                    List<string> strInterest = new List<string>();
                    var answerToDoYouKnowSoftwareNeeds = model.steps[5].elements[0].value;

                    if (answerToDoYouKnowSoftwareNeeds.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    {
                        strInterest = model.steps[6].elements[0].answerOptions.Where(answer => answer.value.Equals("true", StringComparison.OrdinalIgnoreCase) && answer.searchTags?.Count > 0)
                        .Select(g => g.searchTags.FirstOrDefault().ToString()).Distinct().ToList();
                    }
                    else
                    {
                        strInterest = model.steps[7].elements[0].answerOptions.Where(answer => answer.value.Equals("true", StringComparison.OrdinalIgnoreCase) && answer.searchTags?.Count > 0)
                        .Select(g => g.searchTags.FirstOrDefault().ToString()).Distinct().ToList();
                    }

                    // If the user knows what software they need, show CT listing:
                    if (strInterest != null && strInterest.Any())
                    {
                        model.ComparisonToolProducts = await ProcessGetProductList(strInterest);
                        model.ContentKey = _controllerHelper.GetUniqueContentKey(model);
                    }
                }

                SetSessionForm(model);
                SetSessionEmail(model.EmailAnswer);

                return GetViewResult(model, response.Payload);
            }
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("/diagnostic-tool")]
        [Route("/diagnostic-tool/start")]
        [ResponseCache(Duration = CacheDurationInSeconds)]
        public async Task<IActionResult> Start()
        {
            ClearSessionForm();
            ClearSessionEmail();
            var response = await _controllerHelper.CreateForm(FormTypes.DiagnosticTool);
            if (response.Result)
                return GetViewResult(response.Payload);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("/diagnostic-tool/startform")]
        public async Task<IActionResult> StartForm(DiagnosticToolForm model)
        {
            ClearSessionForm();
            var response = await _controllerHelper.Start(model);
            if (response.Result)
                return GetViewResult(model);
            else
                return BadRequest();
        }

        [Route("/diagnostic-tool/summary")]
        public async Task<IActionResult> ChangeAnswers()
        {
            var model = await TryGetSessionForm();
            if (model != default)
            {
                model.ProductCategories = _productCategories;
                model.CurrStep = model.steps.Count + 1;
                return await NextStep(model);
            }
            else
                return await Start();
        }


        protected internal async Task<IList<ComparisonToolProduct>> ProcessGetProductList(IList<string> selectedTags)
        {
            try
            {
                var distinctSlectedTagNames = selectedTags.Distinct().Select(g => g.ToString().ToLower());
                // WARNING: the line you are about to read is ugly!!!
                // This is requried because there is no link between DT tags and the Vendor Product Categories.
                // As a result, the nearest simillarity is the name, but even that is not an exact match
                var taggedProductCategoryList = _productCategories.Where(pc => (distinctSlectedTagNames.Contains(pc.name.ToLower()) || distinctSlectedTagNames.Contains(pc.name.ToLower() + "software") || distinctSlectedTagNames.Contains("digital" + pc.name.ToLower() + "software")));
                var distinctProductCategoryIds = taggedProductCategoryList.Distinct().Select(g => (long)g.id);

                if (_ctDisplayOption.ShowAllProductStatuses ?? false)
                {
                    var productsVm = await _comparisonToolService.GetProducts();
                    return productsVm.Where(p => distinctProductCategoryIds.Contains(p.product_type)).ToList();
                }
                else
                {
                    var productsVm = await _comparisonToolService.GetApprovedProductsFromApprovedVendors();
                    return productsVm.Where(p => distinctProductCategoryIds.Contains(p.product_type)).ToList();
                }
            }
            catch (Exception ex)
            {
                var routeUrl = HttpContext.Request.Headers["Referer"].ToString();
                _logger.LogError(ex, routeUrl + "Error in ProcessGetProductList: " + ex.Message);
                return new List<ComparisonToolProduct>();
            }

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
        [Route("/diagnostic-tool/startform")]
        public async Task<IActionResult> StartForm()
        {
            return await Start();
        }
        #endregion
    }
}