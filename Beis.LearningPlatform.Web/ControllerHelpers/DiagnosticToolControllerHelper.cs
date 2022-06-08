using Beis.LearningPlatform.Data.Entities.Skills;

namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    /// <summary>
    /// A class that defines an implementation of a helper for the Diagnostic Tool controller.
    /// </summary>
    public class DiagnosticToolControllerHelper : ControllerHelperBase, IDiagnosticToolControllerHelper
    {
        private readonly ICmsService2 _cmsService;
        private readonly IDiagnosticToolControllerHelper _controllerHelperInterface;
        private readonly IDiagnosticToolFormService _diagnosticToolFormService;
        private readonly IEmailService _emailService;
        private readonly ISkillsOneService _skillsOneService;
        private readonly ISkillsTwoService _skillsTwoService;
        private readonly ISkillsThreeService _skillsThreeService;
        private readonly IMapper _mapper;
        private readonly VendorAppOption _vendorAppOption;
        private readonly IEmailResponseHelperFactory _emailResponseHelperFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="diagnosticToolFormService">An IDiagnosticToolFormService that is the form service to use.</param>
        /// <param name="cmsService">An ICmsService2 that is the CMS service to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="emailService">An IEmailService that is the email service to use.</param>
        /// <param name="skillsOneService">An ISkillsOneService that is the skills one service to use.</param>
        /// <param name="skillsTwoService">An ISkillsTwoService that is the skills two service to use.</param>
        /// <param name="vendorAppOption">An IOptions&lt;VendorAppOption&gt; that is the vendor option to use.</param>
        /// <param name="emailResponseHelperFactory">An IEmailResponseHelperFactory that is the email response helper factory to use.</param>
        public DiagnosticToolControllerHelper(ILogger<DiagnosticToolControllerHelper> logger,
                                              IDiagnosticToolFormService diagnosticToolFormService,
                                              ICmsService2 cmsService,
                                              IMapper mapper,
                                              IEmailService emailService,
                                              ISkillsOneService skillsOneService,
                                              ISkillsTwoService skillsTwoService,
                                              ISkillsThreeService skillsThreeService,
                                              IOptions<VendorAppOption> vendorAppOption,
                                              IEmailResponseHelperFactory emailResponseHelperFactory,
                                              IHttpContextAccessor httpContextAccessor)
            : base(logger)
        {
            _diagnosticToolFormService = diagnosticToolFormService;
            _cmsService = cmsService;
            _mapper = mapper;
            _emailService = emailService;
            _skillsOneService = skillsOneService;
            _skillsTwoService = skillsTwoService;
            _skillsThreeService = skillsThreeService;
            _vendorAppOption = vendorAppOption.Value;
            _emailResponseHelperFactory = emailResponseHelperFactory ?? throw new ArgumentNullException(nameof(emailResponseHelperFactory));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _controllerHelperInterface = this;            
        }


        private FormValidationError[] CreateValidationErrors(string[] errorMessages, int startID = 1)
        {
            List<FormValidationError> returnValue = new();

            if (errorMessages != null && errorMessages.Length > 0)
            {
                foreach (string errorMessage in errorMessages)
                    returnValue.Add(new FormValidationError() { id = startID++, errorMessage = errorMessage });
            }

            return returnValue.ToArray();
        }

        private async Task LoadRegisterYourInterestBlock(DiagnosticToolForm form, FormSearchTags[] formSearchTags)
        {
            CMSPageComponent block = default;

            if (formSearchTags.Contains(FormSearchTags.ecommerceSoftware))
            {
                var page = await _cmsService.GetPage("diagnostic-tools/interested-in-ecommerce-block");
                if (page?.components?.Count >= 1 && page.components[0].__component == "page.textblock")
                    block = page.components[0];
            }

            form.RegisterYourInterestBlock = block;
        }

        private async Task LoadArticles(DiagnosticToolForm form)
        {
            var selectedTags = form.GetTagsFromSelectedAnswers(true, true);
            var selectedTagNames = selectedTags.Select(x => x.ToString());

            var articles = await _cmsService.GetSearchArticles(selectedTagNames);
            form.Articles = new();
            form.Articles.AddRange(articles);
            form.Articles.Sort();

            await LoadRegisterYourInterestBlock(form, selectedTags);
        }




        async Task<ControllerHelperOperationResponse<DiagnosticToolForm>> IDiagnosticToolControllerHelper.CreateForm(FormTypes formType)
        {
            Guid requestID = RecordRequest();
            DiagnosticToolForm returnValue = _diagnosticToolFormService.LoadNewForm(formType);

            await _controllerHelperInterface.SetNavAndFooter(returnValue);

            return new ControllerHelperOperationResponse<DiagnosticToolForm>(requestID, true, returnValue);
        }

        async Task<ControllerHelperOperationResponse<DiagnosticToolForm>> IDiagnosticToolControllerHelper.GotoStep(DiagnosticToolForm form, int step)
        {
            Guid requestID = RecordRequest();

            if (step > 0 && step <= form.steps.Count)
                form.CurrStep = step;

            await _controllerHelperInterface.SetNavAndFooter(form);

            return new ControllerHelperOperationResponse<DiagnosticToolForm>(requestID, true, form);
        }

        async Task<ControllerHelperOperationResponse<DiagnosticToolForm>> IDiagnosticToolControllerHelper.NextStep(DiagnosticToolForm form, bool isValid, string[] errorMessages)
        {
            var currentStepID = form.CurrStep;
            Guid requestID = RecordRequest();

            if (!form.steps.Where(x => x.id == currentStepID).Any())
                currentStepID = form.steps.Where(x => !x.skipStep).Max(x => x.id);

            form.validationErrors.Clear();
            form.FormIsCompleted = false;

            // Clear error messages from the non-current steps
            form.steps.Where(x => x.id != currentStepID).ClearErrors();

            // Validate the current step
            var currentStep = form.steps.Where(x => x.id == currentStepID).First();
            if (currentStep.elements != null && !currentStep.elements.ValidateElements(out var formValidationErrors))
            {
                // Current step is invalid
                form.validationErrors.AddRange(formValidationErrors);
            }
            else
            {
                int totalSteps = form.steps.Count;

                // Check for final step
                if (currentStepID < totalSteps)
                {
                    // Get the next step
                    int nextStepID = form.steps.GetNextFormStepID(currentStepID);
                    if (nextStepID <= totalSteps)
                    {
                        form.CurrStep = nextStepID;
                    }
                    else
                    {
                        form.validationErrors.AddRange(CreateValidationErrors(errorMessages));
                        if (isValid)
                        {
                            form.FormIsCompleted = true;
                            form.CurrStep = nextStepID;
                        }
                    }
                }
                else
                {
                    form.validationErrors.AddRange(CreateValidationErrors(errorMessages));
                    if (isValid)
                    {
                        form.FormIsCompleted = true;
                        
                        if((int)form.FormType > 0)
                        {
                            //all skills forms, skills1, skills2, and all skills3 forms
                            //add 1 more for last step (summary page), then back btn on summary page gets to the last step
                            form.CurrStep += 1;
                        }

                    }
                }
            }

            await _controllerHelperInterface.SetNavAndFooter(form);

            return new ControllerHelperOperationResponse<DiagnosticToolForm>(requestID, true, form);
        }

        async Task<ControllerHelperOperationResponse<DiagnosticToolForm>> IDiagnosticToolControllerHelper.PreviousStep(DiagnosticToolForm form)
        {
            var currentStepID = form.CurrStep;
            Guid requestID = RecordRequest();

            if (currentStepID > 1)
            {
                currentStepID--;

                // Check if previous step was skipped
                if (currentStepID > 2)
                {
                    var priorStep = form.steps.Where(x => x.id == currentStepID).First();
                    var wasSkipped = priorStep.skipStep;
                    if (wasSkipped)
                        currentStepID--;
                }

                form.CurrStep = currentStepID;
            }

            await _controllerHelperInterface.SetNavAndFooter(form);

            return new ControllerHelperOperationResponse<DiagnosticToolForm>(requestID, true, form);
        }

        ControllerHelperOperationResponse<EmailAnswer> IDiagnosticToolControllerHelper.ProcessEmailAnswer(DiagnosticToolForm form)
        {
            bool isSuccessful = false;
            string message = default;
            string errorHeading = "There is a problem";
            Guid requestID = RecordRequest();
            var emailAnswer = form.EmailAnswer;

            // Reset errors
            emailAnswer.EmailErrorText = default;
            emailAnswer.HasEmailError = false;

            // Check for an email address
            if (emailAnswer.HasEmailAddress)
            {
                string emailErrorText = default;

                // Validate the user accepted the privacy policy
                if (emailAnswer.IsPrivacyPolicyAccepted)
                {
                    // Validate that the email address is valid
                    if (_emailService.IsValidEmailAddress(requestID, emailAnswer.UserEmailAddress).IsValid)
                    {
                        isSuccessful = true;
                    }
                    else
                    {
                        errorHeading = "Incorrect email format";
                        emailErrorText = "Please enter a valid email address (e.g. name@domain.com)";
                        message = "Please enter your email address correctly to continue";
                    }
                }
                else
                {
                    errorHeading = "Check the privacy box";
                    emailErrorText = "Please tick to confirm you've read our privacy policy";
                    message = "To receive a copy of your answers you must tick to confirm you've read our privacy policy";
                }

                // Check whether there is an error
                if (message != default || emailErrorText != default)
                {
                    emailAnswer.ErrorHeading = errorHeading;
                    emailAnswer.EmailErrorText = emailErrorText;
                    emailAnswer.HasEmailError = true;
                }
            }
            else
                isSuccessful = true;

            return new ControllerHelperOperationResponse<EmailAnswer>(requestID, isSuccessful, message, emailAnswer);
        }

        async Task<ControllerHelperOperationResponse<bool>> IDiagnosticToolControllerHelper.ProcessResults(DiagnosticToolForm form, FormTypes formType)
        {
            bool isValid = true;
            Guid requestID = RecordRequest();

            form.validationErrors.Clear();
            form.FormIsCompleted = true;

                // Process email address if there is one
                if (form.EmailAnswer.HasEmailAddress)
                {
                    // Process the email subscription ie. validate email and check Privacy Policy Accepted
                    var processEmailResult = _controllerHelperInterface.ProcessEmailAnswer(form);
                if (!processEmailResult.Result)
                    {
                    isValid = false;
                        // Display the email processing error
                        form.validationErrors.Add(new FormValidationError()
                        {
                            id = 1,
                            errorHeading = processEmailResult.Payload.ErrorHeading,
                            errorMessage = processEmailResult.Message,
                            htmlId = "email"
                        });
                }
            }

            if (isValid)
            {
                ControllerHelperOperationResponse<EmailAnswer> saveDataResult = null;
                if (formType == FormTypes.DiagnosticTool)
                {
                    // Get the result articles
                    await LoadArticles(form);
                    saveDataResult = await SaveDiagnosticToolData(form);
                }
                else if (formType == FormTypes.SkillsOne)
                {
                    saveDataResult = await SaveSkillsOneResponse(form);
                }
                else if (formType == FormTypes.SkillsTwo)
                {
                    await _controllerHelperInterface.UpdateScore(form);
                    saveDataResult = await SaveSkillsTwoResponse(form);
                }
                else if ((int)formType > 2)
                {
                    saveDataResult = await SaveSkillsThreeResponse(form);
                }

                if (saveDataResult?.Result != true)
                { 
                    // Display save data error
                    isValid = false;
                    form.validationErrors.Add(new FormValidationError() { id = 1, errorHeading = "Error Saving Data", errorMessage = saveDataResult.Message });                    
                }
                else if (form.EmailAnswer.HasEmailAddress)
                {
                    // Send the results email to the user
                    var sendEmailResult = await SendResultsEmail(form.EmailAnswer, form);
                    if (!sendEmailResult.Result)
                    {
                        // Display the email sending error
                        isValid = false;
                        form.validationErrors.Add(new FormValidationError() { id = 1, errorHeading = "Error Sending Email", errorMessage = sendEmailResult.Message, htmlId = "email" });
                    }
                }
            }

            await _controllerHelperInterface.SetNavAndFooter(form);
            form.VendorProdLogorUrl = _vendorAppOption.ProdLogoUrl;

            return new ControllerHelperOperationResponse<bool>(requestID, true, isValid);
        }

        private async Task<ControllerHelperOperationResponse<EmailAnswer>> SaveSkillsOneResponse(DiagnosticToolForm form)
        {
            bool isSuccessful = false;
            string message = default;
            Guid requestID = RecordRequest();
            var emailAnswer = form.EmailAnswer;

            // Reset errors
            emailAnswer.EmailErrorText = default;
            emailAnswer.HasEmailError = false;

            try
            {
                var dto = new SkillsOneResponseDto();

                //step 1
                dto.InterestedInNewOpportunities = boolStrToYesNo(form.steps[0].elements[0].answerOptions[0].value);
                dto.InterestedInIncreasingSales = boolStrToYesNo(form.steps[0].elements[0].answerOptions[1].value);
                dto.InterestedInAutomatingTasks = boolStrToYesNo(form.steps[0].elements[0].answerOptions[2].value);
                dto.InterestedInRealTimeData = boolStrToYesNo(form.steps[0].elements[0].answerOptions[3].value);

                //step2
                dto.BiggestFriction = form.steps[1].elements[0].value;

                //step3
                dto.HowMuchSoftwareUsed = form.steps[2].elements[0].value;

                //step4
                dto.ShareInfoInPerson = boolStrToYesNo(form.steps[3].elements[0].answerOptions[0].value);
                dto.ShareInfoSharedDatabase = boolStrToYesNo(form.steps[3].elements[0].answerOptions[1].value);
                dto.ShareInfoMeetings = boolStrToYesNo(form.steps[3].elements[0].answerOptions[2].value);
                dto.ShareInfoInformationConversations = boolStrToYesNo(form.steps[3].elements[0].answerOptions[3].value);
                dto.ShareInfoSomethingElse = boolStrToYesNo(form.steps[3].elements[0].answerOptions[4].value);
                dto.ShareInfoAdditionalInfo = form.steps[3].elements[0].answerOptions[4].additionalInfo;

                //step5
                dto.DigitalAdoption = form.steps[4].elements[0].value;

                var result = await _skillsOneService.SaveSkillsOneResponse(requestID, dto);

                if (result.IsValid)
                    isSuccessful = true;
                else
                {
                    _logger.LogError("{message}", result.Message ?? "Failed to save skills one response");
                    message = result.Message ?? "Failed to save skills one response";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to save skills one response");
                message = "We were unable to save your data.  Please try again";
            }

            return new ControllerHelperOperationResponse<EmailAnswer>(requestID, isSuccessful, message, emailAnswer);
        }

        private async Task<ControllerHelperOperationResponse<EmailAnswer>> SaveSkillsTwoResponse(DiagnosticToolForm form)
        {
            bool isSuccessful = false;
            string message = default;
            Guid requestID = RecordRequest();
            var emailAnswer = form.EmailAnswer;

            // Reset errors
            emailAnswer.EmailErrorText = default;
            emailAnswer.HasEmailError = false;

            try
            {
                var skillsTwoResponse = new SkillsTwoResponse();

                //step 1
                skillsTwoResponse.UseTechnologyForCommunication = form.steps[0].elements[0].value;
                skillsTwoResponse.ImproveCommunication = boolStrToYesNo(form.steps[0].elements[0].answerOptions[3].value);

                //step2
                skillsTwoResponse.UseCloudStorage = form.steps[1].elements[0].value;
                skillsTwoResponse.ImproveDataStorage = boolStrToYesNo(form.steps[1].elements[0].answerOptions[3].value);

                //step3
                skillsTwoResponse.UseOnlinePayments = form.steps[2].elements[0].value;
                skillsTwoResponse.ImprovePayments = boolStrToYesNo(form.steps[2].elements[0].answerOptions[3].value);

                //step4
                skillsTwoResponse.UseOnlineAdvertise = form.steps[3].elements[0].value;
                skillsTwoResponse.ImproveOnlineMarketing = boolStrToYesNo(form.steps[3].elements[0].answerOptions[3].value);

                //step5
                skillsTwoResponse.UseOnlineShop = form.steps[4].elements[0].value;
                skillsTwoResponse.ImproveOnlineBusiness = boolStrToYesNo(form.steps[4].elements[0].answerOptions[3].value);

                //step6
                skillsTwoResponse.UsePersonaliseMessagesToCustomers = form.steps[5].elements[0].value;
                skillsTwoResponse.ImprovePersonalisedMarketing = boolStrToYesNo(form.steps[5].elements[0].answerOptions[3].value);

                //step7
                skillsTwoResponse.UseSoftwareForPlanning = form.steps[6].elements[0].value;
                skillsTwoResponse.ImprovePlanning = boolStrToYesNo(form.steps[6].elements[0].answerOptions[3].value);

                //step8
                skillsTwoResponse.UseDigitalTraining = form.steps[7].elements[0].value;
                skillsTwoResponse.ImproveDigitalTraining = boolStrToYesNo(form.steps[7].elements[0].answerOptions[3].value);

                //step9
                skillsTwoResponse.UseSoftwareForInformationSharing = form.steps[8].elements[0].value;
                skillsTwoResponse.ImproveInformationSharing = boolStrToYesNo(form.steps[8].elements[0].answerOptions[3].value);


                var result = await _skillsTwoService.SaveSkillsTwoResponse(requestID, skillsTwoResponse);

                if (result.IsValid)
                    isSuccessful = true;
                else
                {
                    _logger.LogError("{message}", result.Message ?? "Failed to save skills two response");
                    message = result.Message ?? "Failed to save skills two response";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to save skills two response");
                message = "We were unable to save your data.  Please try again";
            }

            return new ControllerHelperOperationResponse<EmailAnswer>(requestID, isSuccessful, message, emailAnswer);
        }

        private async Task<ControllerHelperOperationResponse<EmailAnswer>> SaveSkillsThreeResponse(DiagnosticToolForm form)
        {
            bool isSuccessful = false;
            string message = default;
            Guid requestID = RecordRequest();
            var emailAnswer = form.EmailAnswer;

            // Reset errors
            emailAnswer.EmailErrorText = default;
            emailAnswer.HasEmailError = false;

            try
            {
                var skillsThreeResponse = new SkillsThreeResponse();
                skillsThreeResponse.Questionnaire = form.userTypeActionPlanSection;

                //step 1
                skillsThreeResponse.WhyNeedStart = form.steps[0].elements[0].answerOptions[0].value;
                skillsThreeResponse.WhyNeedNext = form.steps[0].elements[0].answerOptions[1].value;
                skillsThreeResponse.WhyNeedFinally = form.steps[0].elements[0].answerOptions[2].value;

                //step2
                skillsThreeResponse.HowAccessStart = form.steps[1].elements[0].answerOptions[0].value;
                skillsThreeResponse.HowAccessNext = form.steps[1].elements[0].answerOptions[1].value;
                skillsThreeResponse.HowAccessFinally = form.steps[1].elements[0].answerOptions[2].value;

                //step3
                skillsThreeResponse.RiskStart = form.steps[2].elements[0].answerOptions[0].value;
                skillsThreeResponse.RiskNext = form.steps[2].elements[0].answerOptions[1].value;
                skillsThreeResponse.RiskFinally = form.steps[2].elements[0].answerOptions[2].value;

                var result = await _skillsThreeService.SaveSkillsThreeResponse(requestID, skillsThreeResponse);

                if (result.IsValid)
                    isSuccessful = true;
                else
                {
                    _logger.LogError("{message}", result.Message ?? "Failed to save skills three response");
                    message = result.Message ?? "Failed to save skills three response";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to save skills three response");
                message = "We were unable to save your data.  Please try again";
            }

            return new ControllerHelperOperationResponse<EmailAnswer>(requestID, isSuccessful, message, emailAnswer);
        }
        private async Task<ControllerHelperOperationResponse<EmailAnswer>> SaveDiagnosticToolData(DiagnosticToolForm form)
        {
            bool isSuccessful = false;
            string message = default;
            Guid requestID = RecordRequest();
            var emailAnswer = form.EmailAnswer;

            // Reset errors
            emailAnswer.EmailErrorText = default;
            emailAnswer.HasEmailError = false;

            try
            {
                // Save the user's email address
                var dto = _mapper.Map<DiagnosticToolEmailAnswerDto>(emailAnswer);

                //step 1
                dto._HowSalesTakePlace = form.steps[0].elements[0].value;

                //step 2
                dto._WhichSector = form.steps[1].elements[0].selectedValue;

                //step 3
                dto._SoftwareUsed = form.steps[2].elements[0].value;

                //step 4
                dto._CurrentSupport = form.steps[3].elements[0].value;

                //step 5
                dto._StoreOnPaper = boolStrToYesNo(form.steps[4].elements[0].answerOptions[0].value);
                dto._StoreOnIndividualDrives = boolStrToYesNo(form.steps[4].elements[0].answerOptions[1].value);
                dto._StoreOnSharedDrives = boolStrToYesNo(form.steps[4].elements[0].answerOptions[2].value);
                dto._StoreOnCloud = boolStrToYesNo(form.steps[4].elements[0].answerOptions[3].value);

                //step 6
                dto._SoftwareNeedsKnown = form.steps[5].elements[0].value;

                //step 7
                dto._SoftwareCRM = boolStrToYesNo(form.steps[6].elements[0].answerOptions[0].value);
                dto._SoftwareECommerce = boolStrToYesNo(form.steps[6].elements[0].answerOptions[1].value);
                dto._SoftwareDigitalAccounting = boolStrToYesNo(form.steps[6].elements[0].answerOptions[2].value);
                dto._SoftwareSomethingElse = boolStrToYesNo(form.steps[6].elements[0].answerOptions[3].value);
                dto._SoftwareAdditionalInfo = form.steps[6].elements[0].answerOptions[3].additionalInfo;

                //step 8
                dto._SimplifyPaymentsAndListing = boolStrToYesNo(form.steps[7].elements[0].answerOptions[0].value);
                dto._SimplifySellingViaWebsite = boolStrToYesNo(form.steps[7].elements[0].answerOptions[1].value);
                dto._SimplifyCustomerExperiences = boolStrToYesNo(form.steps[7].elements[0].answerOptions[2].value);
                dto._SimplifyTaxAndAccounting = boolStrToYesNo(form.steps[7].elements[0].answerOptions[3].value);
                dto._SimplifySalesInfo = boolStrToYesNo(form.steps[7].elements[0].answerOptions[4].value);
                dto._SimplifyStockLevels = boolStrToYesNo(form.steps[7].elements[0].answerOptions[5].value);
                dto._SimplifyMonitoringSales = boolStrToYesNo(form.steps[7].elements[0].answerOptions[6].value);
                dto._SimplifyCustomersNeeds = boolStrToYesNo(form.steps[7].elements[0].answerOptions[7].value);
                dto._SimplifyCommunication = boolStrToYesNo(form.steps[7].elements[0].answerOptions[8].value);
                dto._SimplifyNone = boolStrToYesNo(form.steps[7].elements[0].answerOptions[9].value);
                dto._SimplifyAdditionalInfo = form.steps[7].elements[0].answerOptions[9].additionalInfo;

                var result = await _emailService.SaveEmailAddress(requestID, dto);

                if (result.IsValid)
                    isSuccessful = true;
                else
                    throw new InvalidOperationException(result.Message ?? "Failed to save email address");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to save user's email address");
                message = "We were unable to save your email address.  Please try again";
            }

            return new ControllerHelperOperationResponse<EmailAnswer>(requestID, isSuccessful, message, emailAnswer);
        }

        private string boolStrToYesNo(string inputStr)
        {
            if (inputStr == null)
                return inputStr;
            bool.TryParse(inputStr, out bool inputStrB);
            return inputStrB == true ? "Yes" : "No";
        }

        async Task<ControllerHelperOperationResponse> SendResultsEmail(EmailAnswer emailAnswer, DiagnosticToolForm form)
        {
            bool isSuccessful = false;
            string message = default;
            Guid requestID = RecordRequest();

            var helper = _emailResponseHelperFactory.Get(form.FormType);
            var payload =  await helper.ConvertToResultsEmail(form);

            var result = await _emailService.SendResultsRemail(requestID, emailAnswer.UserEmailAddress, payload);
            if (result.IsValid)
                isSuccessful = true;
            else
                message = result.Message;

            return new ControllerHelperOperationResponse(requestID, isSuccessful, message);
        }

        async Task<ControllerHelperOperationResponse> IDiagnosticToolControllerHelper.SetNavAndFooter(DiagnosticToolForm form)
		{
			Guid requestID = RecordRequest();

			var result = await _cmsService.GetPage("diagnostic-tools/start-page");
			if (result != default)
			{
				form.navigations = result.navigations;
				form.side_navigations = result.side_navigations;
				form.footers = result.footers;
			}

			form.ContentKey = GetUniqueContentKey(form);

			return new ControllerHelperOperationResponse(requestID, true);
		}

        /// <summary>
        /// Slightly convoluted logic to get a unique content key for GA
        /// </summary>
		public string GetUniqueContentKey(DiagnosticToolForm form)
		{
			var contentKey = _httpContextAccessor.HttpContext.Request.Path.Value; // This get the nextstep/previous step actions as well.
            if (form == null)
            {
                return contentKey;
            }

			var isPost = _httpContextAccessor.HttpContext.Request.Method == "POST";
			var currentStepId = (isPost && form.steps != null && form.steps.Any()) ? form.CurrStep : default(int?);
            var currentStep = currentStepId.HasValue ? form.steps.SingleOrDefault(x => x.id == currentStepId) : null;
			if (currentStep != null && !form.FormIsCompleted) // If in a formstep get the title
            {
				contentKey = $"{contentKey}-{currentStep.title}-{currentStep.id}";
			}
			else if (form.FormIsCompleted) // Form is completed - summary page:
			{
				contentKey = $"{contentKey}-completed";
			}

            if (form.GetCustomContentKeyValue(out string customKeyValue))
            { 
                contentKey = $"{contentKey}-{customKeyValue}";
            }

			contentKey = contentKey.Replace('/', '-').Replace(' ', '-').Trim(new char[] { ' ', '-' }).UrlEncode(true);
			return contentKey;
		}

		async Task<ControllerHelperOperationResponse> IDiagnosticToolControllerHelper.Start(DiagnosticToolForm form)
        {
            Guid requestID = RecordRequest();

            form.CurrStep = 1;
            form.steps.ClearErrors();
            await _controllerHelperInterface.SetNavAndFooter(form);

            return new ControllerHelperOperationResponse(requestID, true);
        }

        Task IDiagnosticToolControllerHelper.UpdateScore(DiagnosticToolForm submittedForm)
        {
            DiagnosticToolForm mainForm = _diagnosticToolFormService.LoadNewForm(FormTypes.SkillsTwo);

            foreach (var submittedFormStep in submittedForm.steps)
            {
                var step = mainForm.steps.SingleOrDefault(s => s.id == submittedFormStep.id);
                if (step != null)
                {
                    var element = step.elements.SingleOrDefault(e => e.id == submittedFormStep.elements[0].id);
                    if (element != null)
                    {
                        var answerOption = element.answerOptions.FirstOrDefault(option => option.value.Equals(submittedFormStep.elements[0].value));
                        if(answerOption != null)
                        {
                            submittedForm.TotalScore = submittedForm.TotalScore + answerOption.score;
                        }   
                    }
                }
            }

            if(submittedForm.TotalScore <= 14)
            {
                submittedForm.SkilledModuleTwoResultType = Library.Enums.SkilledModuleTwoResultType.DigitalNewComer;
            }
            else if (submittedForm.TotalScore > 15 && submittedForm.TotalScore <= 20)
            {
                submittedForm.SkilledModuleTwoResultType = Library.Enums.SkilledModuleTwoResultType.DigitalMover;
            }
            else
            {
                submittedForm.SkilledModuleTwoResultType = Library.Enums.SkilledModuleTwoResultType.DigitalPerfomer;
            }

            return Task.CompletedTask;
        }
    }
}