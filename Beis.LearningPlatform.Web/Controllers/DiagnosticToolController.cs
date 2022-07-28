namespace Beis.LearningPlatform.Web.Controllers
{
    /// <summary>
    /// A class that defines a controller for the Diagnostic Tool.
    /// </summary>
    public class DiagnosticToolController : FormControllerBase
    {
        private const int CacheDurationInSeconds = 120;
        
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
            : base(logger, controllerHelper)
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

        [HttpPost]
        [Route("/diagnostic-tool/nextstep")]
        public async override Task<IActionResult> NextStep(DiagnosticToolForm model)
        {
            return await base.NextStep(model);
        }

        [HttpPost]
        [Route("/diagnostic-tool/gotostep")]
        public async override Task<IActionResult> GoToStep(DiagnosticToolForm model, [FromQuery] int step)
        {
            return await base.GoToStep(model, step);
        }

        [HttpPost]
        [Route("/diagnostic-tool/prevstep")]
        public async override Task<IActionResult> PrevStep(DiagnosticToolForm model)
        {
            return await base.PrevStep(model);
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
        public async override Task<IActionResult> Start()
        {
            ClearSessionForm();
            ClearSessionEmail();
            var response = await _controllerHelper.CreateForm(FormTypes.DiagnosticTool);
            if (response.Result)
            {
                response.Payload.title = "Help to Grow find your software solution - Gov.Uk";
                return GetViewResult(response.Payload);
            }
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("/diagnostic-tool/startform")]
        public async override Task<IActionResult> StartForm(DiagnosticToolForm model)
        {
            return await base.StartForm(model);
        }

        [Route("/diagnostic-tool/summary")]
        public async override Task<IActionResult> ChangeAnswers()
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
                _logger.LogError(ex, "{routeUrl} Error in ProcessGetProductList: {message}", HttpContext.Request.Headers["Referer"].ToString(), ex.Message);
                return new List<ComparisonToolProduct>();
            }

        }

        [Route("/diagnostic-tool/startform")]
        public async override Task<IActionResult> StartForm()
        {
            return await Start();
        }
    }
}