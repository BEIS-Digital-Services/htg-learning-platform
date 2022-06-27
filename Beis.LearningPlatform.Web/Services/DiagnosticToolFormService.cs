namespace Beis.LearningPlatform.Web.Services
{
    /// <summary>
    /// A class that implements a Diagnostic Tool Form service.
    /// </summary>
    public class DiagnosticToolFormService : IDiagnosticToolFormService
    {
        static DiagnosticToolFormService()
        {
            _step2ListItems = new()
            {
                { 211, "Agriculture, Forestry and Fishing" },
                { 212, "Mining, Quarrying and Utilities" },
                { 213, "Manufacturing" },
                { 214, "Construction" },
                { 215, "Wholesale and Retail Trade" },
                { 216, "Transportation and Storage" },
                { 217, "Accommodation and Food Service" },
                { 218, "Information and Communication" },
                { 219, "Financial and Insurance" },
                { 220, "Real Estate" },
                { 221, "Professional, Scientific and Technical" },
                { 222, "Administrative and Support Service" },
                { 223, "Public Administration and Defence" },
                { 224, "Education" },
                { 225, "Health and Social Work" },
                { 226, "Arts, Entertainment and Recreation" },
                { 227, "Other services" }
            };
        }


        public DiagnosticToolFormService(ILogger<DiagnosticToolFormService> logger,
            IOptions<ComparisonToolDisplayOption> ctDisplayOptions, IOptions<ApplicationForm> options)
        {
            _logger = logger;
            _ctDisplayOption = ctDisplayOptions.Value;
            _applicationForm = options.Value;
        }

        private readonly ILogger _logger;
        private static readonly Dictionary<int, string> _step2ListItems;
        private readonly ApplicationForm _applicationForm;
        private readonly ComparisonToolDisplayOption _ctDisplayOption;

        private FormAnswerOptionElement GenerateAnswerOption(FormDisplayControlType controlType, int id, string strongText, string text, string hint, string value, string htmlId, string htmlName, string resultPageLabel, FormStepElement parent, params FormSearchTags[] formSearchTags)
        {
            return new()
            {
                controlType = controlType,
                hint = hint,
                id = id,
                parent = parent,
                ResultPageLabel = resultPageLabel ?? string.Empty,
                searchTags = formSearchTags != null ? formSearchTags.ToList() : new List<FormSearchTags>(),
                strongText = strongText,
                text = text,
                textControlHtmlId = htmlId,
                textControlHtmlName = htmlName,
                value = value
            };
        }
        private FormAnswerOptionElement GenerateAnswerOptionCheckbox(int id, string text, string htmlId, string htmlName, string value, string resultPageLabel, FormStepElement parent, params FormSearchTags[] formSearchTags)
        {
            return GenerateAnswerOption(FormDisplayControlType.Checkbox, id, "", text, text, value, htmlId, htmlName, resultPageLabel, parent, formSearchTags);
        }

        private FormAnswerOptionElement GenerateAnswerOptionListItem(int id, string text, string hint, string htmlId, string htmlName, FormStepElement parent, params FormSearchTags[] formSearchTags)
        {
            return GenerateAnswerOption(FormDisplayControlType.ListItem, id, "", text, hint, text, htmlId, htmlName, default, parent, formSearchTags);
        }

        private FormAnswerOptionElement GenerateAnswerOptionRadio(int id, string text, string htmlId, string htmlName, FormStepElement parent, params FormSearchTags[] formSearchTags)
        {
            return GenerateAnswerOption(FormDisplayControlType.Radio, id, "", text, text, text, htmlId, htmlName, default, parent, formSearchTags);
        }

        private FormStepElement GenerateElement(FormDisplayControlType controlType, int id, string text, string text2, string subText, string nextButtonText, string hint, string hint2, string htmlId, string htmlName, string value = default, bool showHintInBold = false, bool showHint2InBold = false)
        {
            return new()
            {
                controlType = controlType,
                hint = hint,
                showHintInBold = showHintInBold,
                hint2 = hint2,
                showHint2InBold = showHint2InBold,
                id = id,
                text = text,
                text2 = text2,
                subText = subText,
                nextButtonText = nextButtonText,
                textControlHtmlId = htmlId,
                textControlHtmlName = htmlName,
                value = value ?? string.Empty
            };
        }

        private FormStepElement GenerateElementCheckboxGroup(int id, string text, string hint, string htmlId, string htmlName, string value = default, params FormAnswerOptionElement[] answers)
        {
            FormStepElement returnValue = GenerateElement(FormDisplayControlType.CheckboxGroup, id, text, "", null, hint, "", htmlId, htmlName, value);

            if (answers?.Length > 0)
                foreach (var answer in answers)
                {
                    returnValue.answerOptions.Add(answer);
                    answer.parent = returnValue;
                }

            return returnValue;
        }

        private FormStepElement GenerateElementList(int id, string text, string hint, string htmlId, string htmlName, Dictionary<int, string> listItems = default, bool sortList = true)
        {
            FormStepElement returnValue = GenerateElement(FormDisplayControlType.List, id, text, "", "", null, hint, "", htmlId, htmlName);

            if (listItems?.Count > 0)
            {
                if (sortList)
                {
                    foreach (var item in listItems.OrderBy(kvp => kvp.Value))
                        returnValue.answerOptions.Add(GenerateAnswerOptionListItem(item.Key, item.Value, hint, $"{htmlId}Option", htmlName, returnValue));
                }
                else
                {
                    foreach (var item in listItems)
                        returnValue.answerOptions.Add(GenerateAnswerOptionListItem(item.Key, item.Value, hint, $"{htmlId}Option", htmlName, returnValue));
                }
            }

            return returnValue;
        }

        private FormStepElement GenerateElementRadioGroup(int id, string text, string hint, string htmlId, string htmlName, string value = default, params FormAnswerOptionElement[] answers)
        {
            FormStepElement returnValue = GenerateElement(FormDisplayControlType.RadioGroup, id, text, "", "", null, hint, "", htmlId, htmlName, value);

            if (answers?.Length > 0)
                foreach (var answer in answers)
                {
                    returnValue.answerOptions.Add(answer);
                    answer.parent = returnValue;
                }

            return returnValue;
        }

        private FormStep GenerateFormStep(int id, int order, string title, string summaryTitle = null, bool showInSummary = true, bool createElements = true)
        {
            return new()
            {
                elements = createElements ? new List<FormStepElement>() : default,
                id = id,
                title = title,
                summaryTitle = summaryTitle,
                titleControlType = FormDisplayControlType.Label,
                order = order,
                orderControlType = FormDisplayControlType.Label,
                showInSummary = showInSummary
            };
        }

        // Radio
        private FormStep GetStep1()
        {
            var returnValue = GenerateFormStep(1, 1, "About your business");

            var stepElement = GenerateElementRadioGroup(100, "Where do most of your sales take place?", "Select ONE ONLY", "lblMultipleChoice", "Question5", default,
                                                        GenerateAnswerOptionRadio(101, "Online", "Question5Option1", "Question5", default, FormSearchTags.crm, FormSearchTags.ecommerce, FormSearchTags.onlineTrading),
                                                        GenerateAnswerOptionRadio(102, "In person", "Question5Option2", "Question5", default, FormSearchTags.crm, FormSearchTags.inPersonTrading),
                                                        GenerateAnswerOptionRadio(103, "Online and in person", "Question5Option3", "Question5", default, FormSearchTags.crm, FormSearchTags.ecommerce, FormSearchTags.inPersonTrading, FormSearchTags.onlineTrading));
            returnValue.elements.Add(stepElement);

            return returnValue;
        }


        // List - Dropdown
        private FormStep GetStep2()
        {
            var returnValue = GenerateFormStep(2, 2, "About your business", string.Empty, false);
            returnValue.elements.Add(GenerateElementList(210, "Which sector do you operate in?", "sector", "lstIndustry", "lstIndustry", _step2ListItems));
            return returnValue;
        }

        // Radio
        private FormStep GetStep3()
        {
            var returnValue = GenerateFormStep(3, 3, "How you use software");

            FormAnswerOptionElement choice1 = GenerateAnswerOptionRadio(301, "We don't use software", "Question5Option1", "Question5", default, FormSearchTags.beginner);
            choice1.hint = "VeryLittle|UseMore";
            choice1.ResultPageLabel = "want to start using software in your business";

            FormAnswerOptionElement choice2 = GenerateAnswerOptionRadio(302, "We use some software and want more", "Question5Option2", "Question5", default, FormSearchTags.intermediate);
            choice2.hint = "MediumUse|UseMore";
            choice2.ResultPageLabel = "use some software and want more";

            FormAnswerOptionElement choice3 = GenerateAnswerOptionRadio(303, "All processes are digital. We’re looking for replacements", "Question5Option3", "Question5", default, FormSearchTags.expert);
            choice3.hint = "AlwaysUse|Upgrades";
            choice3.ResultPageLabel = "already use digital ways of working and are looking for replacements";

            var stepElement = GenerateElementRadioGroup(300, "How does your business currently use software?", "Select ONE ONLY", "lblMultipleChoice", "Question5", default, choice1, choice2, choice3);
            returnValue.elements.Add(stepElement);

            return returnValue;
        }

        // Radio
        private FormStep GetStep4()
        {
            var returnValue = GenerateFormStep(4, 4, "Your IT set up");

            FormAnswerOptionElement choice1 = GenerateAnswerOptionRadio(401, "We have someone / an in-house team", "Question5Option1", "Question5", default);
            choice1.hint = "In house";

            FormAnswerOptionElement choice2 = GenerateAnswerOptionRadio(402, "We have an external team who support us", "Question5Option2", "Question5", default);
            choice2.hint = "External Company";

            FormAnswerOptionElement choice3 = GenerateAnswerOptionRadio(403, "We rely on supplier support", "Question5Option3", "Question5", default);
            choice3.hint = "Software Supplier";

            FormAnswerOptionElement choice4 = GenerateAnswerOptionRadio(404, "We don't have any support in place", "Question5Option4", "Question5", default);
            choice4.hint = "No Support";

            FormAnswerOptionElement choice5 = GenerateAnswerOptionRadio(405, "Friends/family help help when we need it", "Question5Option5", "Question5", default);
            choice5.hint = "Friends/Family";

            var stepElement = GenerateElementRadioGroup(400, "What kind of IT support do you have in place?", "Select ONE ONLY", "lblMultipleChoice", "Question5", default, choice1, choice2, choice3, choice4, choice5);
            returnValue.elements.Add(stepElement);

            return returnValue;
        }

        // Checkboxes
        private FormStep GetStep5()
        {
            var returnValue = GenerateFormStep(5, 5, "Your IT set up");

            var stepElement = GenerateElementCheckboxGroup(510, "How do you currently store your business information?", "Select all that apply", "lblMultipleChoice", "Question5", default,
                                                           GenerateAnswerOptionCheckbox(511, "On stickies and in notebooks", "Question5Option1", "Question5", "false", default, default, FormSearchTags.analoguePractices),
                                                           GenerateAnswerOptionCheckbox(512, "On individual computer hard drives", "Question5Option2", "Question5", "false", default, default, FormSearchTags.digitalPractices),
                                                           GenerateAnswerOptionCheckbox(513, "On shared hard drives", "Question5Option3", "Question5", "false", default, default, FormSearchTags.digitalPractices),
                                                           GenerateAnswerOptionCheckbox(514, "On Cloud storage (e.g. iCloud)", "Question5Option4", "Question5", "false", default, default, FormSearchTags.digitalPractices));
            returnValue.elements.Add(stepElement);

            return returnValue;
        }

        // Radio
        private FormStep GetStep6()
        {
            var returnValue = GenerateFormStep(6, 6, "What you need & want to achieve");

            var stepElement = GenerateElementRadioGroup(600, "Do you know which software you need?", "Select ONE ONLY", "lblMultipleChoice", "Question5", default,
                                                        GenerateAnswerOptionRadio(601, "Yes", "Question5Option1", "Question5", default),
                                                        GenerateAnswerOptionRadio(602, "No", "Question5Option2", "Question5", default));
            returnValue.elements.Add(stepElement);

            return returnValue;
        }

        // Checkboxes
        private FormStep GetStep7()
        {
            var returnValue = GenerateFormStep(7, 7, "What you need & want to achieve", string.Empty, false);
            returnValue.skipConditionValue = "No";
            returnValue.skippedByElementId = 600;
            returnValue.skippedByElementStepId = 6;

            var catchAll = GenerateAnswerOptionCheckbox(713, "Something else", "Question12Option4", "Question12", "false", default, default);
            catchAll.additionalInfoRequired = true;
            catchAll.additionalInfoText = "If your software type isn’t listed here, please tell us the type of software you’re interested in.";

            var stepElement = GenerateElementCheckboxGroup(700, "What type of software are you looking for?", "Select all that apply", "chkBoxGroup1", "chkBoxGroup1", default,
                                                           GenerateAnswerOptionCheckbox(710, "Customer relationship management (CRM)", "Question12Option1", "Question12", "false", "CRM Software", default, FormSearchTags.crmSoftware),
                                                           GenerateAnswerOptionCheckbox(711, "eCommerce", "Question12Option2", "Question12", "false", "eCommerce Software", default, FormSearchTags.ecommerceSoftware),
                                                           GenerateAnswerOptionCheckbox(712, "Digital accounting", "Question12Option3", "Question12", "false", "Digital Accounting Software", default, FormSearchTags.accountingSoftware),
                                                           catchAll);
            returnValue.elements.Add(stepElement);

            return returnValue;
        }

        // Checkboxes
        private FormStep GetStep8()
        {
            var returnValue = GenerateFormStep(8, 8, "What you need & want to achieve", string.Empty, false);
            returnValue.skipConditionValue = "Yes";
            returnValue.skippedByElementId = 600;
            returnValue.skippedByElementStepId = 6;

            var catchAll = GenerateAnswerOptionCheckbox(820, "None of these", "Question12Option11", "Question12", "false", default, default);
            catchAll.additionalInfoRequired = true;
            catchAll.additionalInfoText = "If your goal isn’t listed here, please tell us what you’re looking to achieve.";

            var stepElement = GenerateElementCheckboxGroup(800, "Which tasks do you want to simplify?", "Select all that apply", "chkBoxGroup1", "chkBoxGroup1", default,
                                                           GenerateAnswerOptionCheckbox(810, "Taking payments and listing products for sale", "Question12Option1", "Question12", "false", "taking payments and listing products for sale", default, FormSearchTags.ecommerceSoftware),
                                                           GenerateAnswerOptionCheckbox(811, "Selling through the business' website", "Question12Option2", "Question12", "false", "selling through the business website", default, FormSearchTags.ecommerceSoftware),
                                                           GenerateAnswerOptionCheckbox(812, "Creating personalised, online customer experiences", "Question12Option3", "Question12", "false", "creating personal, online customer experiences", default, FormSearchTags.ecommerceSoftware),
                                                           GenerateAnswerOptionCheckbox(813, "Digital tax and accounting", "Question12Option4", "Question12", "false", "digital tax and accounting", default, FormSearchTags.accountingSoftware),
                                                           GenerateAnswerOptionCheckbox(814, "Managing sales info, receipts and invoices", "Question12Option5", "Question12", "false", "managing sales info, receipts, and invoices", default, FormSearchTags.accountingSoftware),
                                                           GenerateAnswerOptionCheckbox(815, "Tracking stock levels", "Question12Option6", "Question12", "false", "tracking stock levels", default, FormSearchTags.accountingSoftware),
                                                           GenerateAnswerOptionCheckbox(816, "Monitoring sales and promotions", "Question12Option7", "Question12", "false", "monitoring sales and promotions", default, FormSearchTags.crmSoftware),
                                                           GenerateAnswerOptionCheckbox(817, "Understanding customer needs and trends", "Question12Option8", "Question12", "false", "understanding customers' needs and trends", default, FormSearchTags.crmSoftware),
                                                           GenerateAnswerOptionCheckbox(818, "Tracking communication between the team and customers", "Question12Option9", "Question12", "false", "tracking communication between the team and customers", default, FormSearchTags.crmSoftware),
                                                           //GenerateAnswerOptionCheckbox(819, "Make promotions easier to plan and run", "Question12Option10", "Question12", "false", "making promotions easier to plan and run", default),
                                                           catchAll);
            returnValue.elements.Add(stepElement);

            return returnValue;
        }


        private string GetOptionValue(OptionElement option)
        {
            string value;
            if (option.ControlType == FormDisplayControlType.Checkbox)
                value = "false";
            else if (option.ControlType == FormDisplayControlType.Textarea)
                value = "";
            else if (string.IsNullOrEmpty(option.Value) == false)
                value = option.Value;
            else
                value = option.Text;

            return value;
        }
        public FormStep GetFormStep(Step step)
        {
            FormStep formStep = GenerateFormStep(step.Id, step.Order, step.Title, step.SummaryTitle, step.ShowInSummary);
            foreach(var element in step.Elements)
            {
                FormStepElement stepElement = GenerateElement(element.ControlType, element.Id, element.Text, element.Text2, element.SubText, element.NextButtonText, element.Hint, element.Hint2, element.TextControlHtmlId, element.TextControlHtmlName, default, element.ShowHintInBold, element.ShowHint2InBold);
                
                var answers = element.OptionElements.Select((option) =>
                {
                    var searchTags = (option.SearchTags != null ? option.SearchTags : new List<FormSearchTags>()).ToArray();
                    string value = GetOptionValue(option);
                    FormAnswerOptionElement choice = GenerateAnswerOption(option.ControlType, option.Id, option.StrongText, option.Text, option.Text, value, option.TextControlHtmlId, option.TextControlHtmlName, default, null, searchTags);
                    choice.hint = option.Hint;
                    choice.ResultPageLabel = option.ResultPageLabel;
                    choice.additionalInfoRequired = option.AdditionalInfoRequired;
                    choice.additionalInfoText = option.AdditionalInfoText;
                    choice.topDownBorder = option.TopDownBorder;
                    choice.score = option.Score;
                    return choice;
                });

                if (answers?.Count() > 0)
                {
                    foreach (var answer in answers)
                    {
                        stepElement.answerOptions.Add(answer);
                        answer.parent = stepElement;
                    }
                }

                formStep.elements.Add(stepElement);
            }
            if (step.SkipConditionValue != null)
            {
                formStep.skipConditionValue = step.SkipConditionValue;
            }
            if (step.SkippedByElementId != null)
            {
                formStep.skippedByElementId = step.SkippedByElementId;
            }
            if (step.SkippedByElementStepId != null)
            {
                formStep.skippedByElementStepId = step.SkippedByElementStepId;
            }
            return formStep;
        }

        public DiagnosticToolForm LoadNewForm(FormTypes formType)
        {
            DiagnosticToolForm returnValue = new()
            {
                CurrStep = 0,
                id = 1,
                title = "Help to Grow: Digital - find your business software solution",
                FormType = formType
            };

            if (_ctDisplayOption.LoadFormFromJson.HasValue && _ctDisplayOption.LoadFormFromJson.Value)
            {
                ApplicationFormType appForm;
                switch (formType)
                {
                    case FormTypes.DiagnosticTool:
                        appForm = _applicationForm.DiagnosticTool;
                        break;
                    case FormTypes.SkillsOne:
                        appForm = _applicationForm.SkillsOne;
                        break;
                    case FormTypes.SkillsTwo:
                        appForm = _applicationForm.SkillsTwo;
                        break;
                    case FormTypes.SkillsThreeNewcomerPlanning:
                        appForm = _applicationForm.SkillsThreeNewcomerPlanning;
                        break;
                    case FormTypes.SkillsThreeNewcomerCommunication:
                        appForm = _applicationForm.SkillsThreeNewcomerCommunication;
                        break;
                    case FormTypes.SkillsThreeNewcomerSupport:
                        appForm = _applicationForm.SkillsThreeNewcomerSupport;
                        break;
                    case FormTypes.SkillsThreeNewcomerTraining:
                        appForm = _applicationForm.SkillsThreeNewcomerTraining;
                        break;
                    case FormTypes.SkillsThreeNewcomerTesting:
                        appForm = _applicationForm.SkillsThreeNewcomerTesting;
                        break;
                    case FormTypes.SkillsThreeMoverPlanning:
                        appForm = _applicationForm.SkillsThreeMoverPlanning;
                        break;
                    case FormTypes.SkillsThreeMoverCommunication:
                        appForm = _applicationForm.SkillsThreeMoverCommunication;
                        break;
                    case FormTypes.SkillsThreeMoverSupport:
                        appForm = _applicationForm.SkillsThreeMoverSupport;
                        break;
                    case FormTypes.SkillsThreeMoverTraining:
                        appForm = _applicationForm.SkillsThreeMoverTraining;
                        break;
                    case FormTypes.SkillsThreeMoverTesting:
                        appForm = _applicationForm.SkillsThreeMoverTesting;
                        break;
                    case FormTypes.SkillsThreePerformerPlanning:
                        appForm = _applicationForm.SkillsThreePerformerPlanning;
                        break;
                    case FormTypes.SkillsThreePerformerCommunication:
                        appForm = _applicationForm.SkillsThreePerformerCommunication;
                        break;
                    case FormTypes.SkillsThreePerformerSupport:
                        appForm = _applicationForm.SkillsThreePerformerSupport;
                        break;
                    case FormTypes.SkillsThreePerformerTraining:
                        appForm = _applicationForm.SkillsThreePerformerTraining;
                        break;
                    case FormTypes.SkillsThreePerformerTesting:
                        appForm = _applicationForm.SkillsThreePerformerTesting;
                        break;


                    default:
                        appForm = _applicationForm.DiagnosticTool;
                        break;
                }

                returnValue.canChangeAnswers = appForm.CanChangeAnswers;
                returnValue.summaryHeading = appForm.SummaryHeading;

                returnValue.backButton = appForm.BackButton;
                returnValue.backLink = appForm.BackLink;
                returnValue.backURLfromQ1 = appForm.BackURLfromQ1;
                returnValue.userTypeActionPlanSection = appForm.UserTypeActionPlanSection;
                returnValue.formLogo = appForm.FormLogo;

                foreach (var step in appForm.Steps)
                {
                    returnValue.steps.Add(GetFormStep(step));
                }
            }
            else
            {
                //for dt when json is not available
                returnValue.backButton = true;
                returnValue.steps.Add(GetStep1()); // STEP 1: HOW YOU TRADE
                returnValue.steps.Add(GetStep2()); // STEP 2: BUSINESS SECTOR  **** WARNING DO NOT CHANGE WITHOUT MAKING THE SAME CHANGE IN RESULT.CSHTML
                returnValue.steps.Add(GetStep3()); // STEP 3: HOW YOU USE SOFTWARE **** WARNING DO NOT CHANGE WITHOUT MAKING THE SAME CHANGE IN RESULT.CSHTML
                returnValue.steps.Add(GetStep4()); // STEP 4: IT SUPPORT
                returnValue.steps.Add(GetStep5()); // STEP 5: HOW YOU STORE INFO
                returnValue.steps.Add(GetStep6()); // STEP 6: KNOW WHICH SOFTWARE YOU NEED?  **** WARNING DO NOT CHANGE WITHOUT MAKING THE SAME CHANGE IN RESULT.CSHTML
                returnValue.steps.Add(GetStep7()); // STEP 7: SELECT SOFTWARE NEEDED
                returnValue.steps.Add(GetStep8()); // STEP 8: WHAT YOU WANT TO ACHIEVE
            }
            return returnValue;
        }
    }
}