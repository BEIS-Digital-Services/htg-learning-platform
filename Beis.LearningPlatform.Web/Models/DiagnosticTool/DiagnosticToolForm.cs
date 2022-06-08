namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    /// <summary>
    /// A class that defines a Diagnostic Tool Form.
    /// </summary>
    public class DiagnosticToolForm : IPageViewModel
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public DiagnosticToolForm()
        {
            EmailAnswer = new EmailAnswer();
            instructions = new List<FormInstruction>();
            steps = new List<FormStep>();
            validationErrors = new List<FormValidationError>();
        }

        public List<CMSSearchArticle> Articles { get; set; }

        public DateTime created_at { get; set; }

        public int CurrStep { get; set; }

        /// <summary>
        /// Gets or sets the email answer associated with the form.
        /// </summary>
        public EmailAnswer EmailAnswer { get; set; }

        public IList<CMSPageFooter> footers { get; set; }

        public string htmlId { get; set; }

        [Required]
        public int id { get; set; }

        public IList<FormInstruction> instructions { get; set; }

        public bool javscriptEnabled { get; set; }

        public IList<CMSPageNavigation> navigations { get; set; }

        public DateTime published_at { get; set; }

        public IList<CMSPageSideNavigation> side_navigations { get; set; }

        public IList<FormStep> steps { get; set; }
        public bool backButton { get; set; }
        public bool backLink { get; set; }
        public string backURLfromQ1 { get; set; }
        public string userTypeActionPlanSection { get; set; }
        public string formLogo { get; set; }

        [Required]        
        public string title { get; set; } 

        public string summaryHeading { get; set; }

        public bool canChangeAnswers { get; set; }

        public DateTime updated_at { get; set; }

        public List<FormValidationError> validationErrors { get; set; }

        public FormSearchTags[] selectedTags => this.GetTagsFromSelectedAnswers();

        /// <summary>
        /// Gets or sets an indication that the form is completed.
        /// </summary>
        public bool FormIsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the register your interest block to display on the Diagnostic Tool results page.
        /// </summary>
        public CMSPageComponent RegisterYourInterestBlock { get; set; }
        public IList<ComparisonToolProduct> ComparisonToolProducts { get; set; }        

        public string pageTitle { get; set; } = "Help to Grow: Digital - Diagnostic Tool";
        public string pagename { get; set; } = "Find your software";
        public bool ShowLayoutNavigation => this.ShowLayoutNavigation();
        public PartialNavigationModel NavigationModel => this.GetPartialNavigationModel();
        public string CmsBaseUrl { get; set; }

        public string VendorProdLogorUrl { get; set; }
        public IList<CMSSearchTag> ProductCategories { get; set; }

        public bool IsInstructionsPage { get; set; }
        public ElementInfo CurrentElement { get; set; }

        // SEO properties:
        public string description { get; } = "Use our digital diagnostic tool to help find out what software is the best for your business, including CRM, Digital Accountancy and eCommerce products.";
        public string meta { get; } = "going digital, planning, budget";
        public bool? index { get; } = true;
        public bool? follow { get; } = true;

		public string ContentKey { get; set; }
		public FormTypes FormType { get; set; }

        public int TotalScore { get; set; }

        public SkilledModuleTwoResultType SkilledModuleTwoResultType { get; set; }
        
        public IList<CMSSearchTag> Tags => ProductCategories?.Any() == true ? ProductCategories : new List<CMSSearchTag>();

        public IList<ComparisonToolProduct> GetAccountingProducts()
        {
            return ComparisonToolProducts?.Where(item => item.product_type == Tags.FirstOrDefault(t => t.name == "accounting")!.id).ToList();
        }

        public IList<ComparisonToolProduct> GetCrmProducts()
        {
            return ComparisonToolProducts?.Where(item => item.product_type == Tags.FirstOrDefault(t => t.name == "crm")!.id).ToList();
        }

        public IList<ComparisonToolProduct> GetECommerceProducts()
        {
            var productType = Tags.FirstOrDefault(t => t.name == "ecommerce")?.id;
            return ComparisonToolProducts?.Where(item => item.product_type == productType).ToList();
        }

        public IList<CMSSearchArticle> GetRelatedArticles()
        {
            // Collect distinct tags
            var distinctTags = selectedTags.Distinct().Select(g => g.ToString()).ToList();
            if (!distinctTags.Any()) return default;

            bool TagToMatch(CMSSearchTag tag) => distinctTags.Contains(tag.name);
            return Articles?.Where(article => article.tags.Exists(TagToMatch)).OrderBy(x => x.order).ToList();
        }

        public IList<FormSearchTags> GetDistinctTagsFromQuestion7()
        {
            return steps[7].elements[0].answerOptions.Where(answer => answer.value.Equals("true", StringComparison.OrdinalIgnoreCase) && answer.searchTags?.Count > 0)
                .Select(g => g.searchTags.FirstOrDefault()).Distinct().ToList();
        }

        public bool IsQuestion1Correct
        {
            get
            {
                var questionHowDoYouTrade = steps[0].elements[0].text;
                return !string.IsNullOrWhiteSpace(questionHowDoYouTrade) && questionHowDoYouTrade.Equals("Where do most of your sales take place?", StringComparison.OrdinalIgnoreCase);
            }
        }

        public bool IsQuestion2Correct
        {
            get
            {
                var questionSector = steps[1].elements[0].text;
                return !string.IsNullOrWhiteSpace(questionSector) && questionSector.Equals("Which sector do you operate in?", StringComparison.OrdinalIgnoreCase);
            }
        }

        public string AnswerHowDoYouUseSoftware
        {
            get
            {
                return steps[2].elements[0].answerOptions.FirstOrDefault(answer => answer.value == steps[2].elements[0].value)?.ResultPageLabel;
            }
        }

        public bool IsAnswerYes()
        {
            var answerToDoYouKnowSoftwareNeeds = steps[5].elements[0].value;
            return answerToDoYouKnowSoftwareNeeds?.Equals("yes", StringComparison.OrdinalIgnoreCase) ?? false;
        }

        public bool IsQuestion6Correct()
        {
            var questionDoYouKnowSoftwareNeeds = steps[5].elements[0].text;
            return !string.IsNullOrWhiteSpace(questionDoYouKnowSoftwareNeeds) && questionDoYouKnowSoftwareNeeds.Equals("Do you know which software you need?", StringComparison.OrdinalIgnoreCase);
        }

        public string InterestsSummary()
        {

            var interests = new List<string>();
            // Collate user interest from Question 7 or 8, depending on answer to question 6
            if (IsQuestion6Correct())
            {
                if (IsAnswerYes())
                {
                    interests.AddRange(from answer in steps[6].elements[0].answerOptions where answer.value.Equals("true", StringComparison.OrdinalIgnoreCase) select string.IsNullOrWhiteSpace(answer.additionalInfo) ? answer.ResultPageLabel : answer.additionalInfo);
                }
                else
                {
                    interests.AddRange(from answer in steps[7].elements[0].answerOptions where answer.value.Equals("true", StringComparison.OrdinalIgnoreCase) select string.IsNullOrWhiteSpace(answer.additionalInfo) ? answer.ResultPageLabel : answer.additionalInfo);
                }

            }

            return ListJoinFormatter.ReplaceLastCharacterWith(string.Join("; ", interests), ";", "and");
        }

        public void ResetSteps()
        {
            // Reset the values on the Skipped Step
            var skippedSteps = steps.Where(step => step.skipStep);
            foreach (var step in skippedSteps)
            {
                foreach (var element in step.elements)
                {
                    foreach (var answer in element.answerOptions)
                    {
                        switch (answer.controlType)
                        {
                            case FormDisplayControlType.Text:
                            {
                                answer.value = null;
                                break;
                            }
                            case FormDisplayControlType.ListItem:
                            case FormDisplayControlType.Radio:
                            {
                                element.selectedValue = null;
                                element.value = null;
                                answer.additionalInfo = null;
                                break;
                            }
                            case FormDisplayControlType.Checkbox:
                            {
                                answer.value = false.ToString();
                                answer.additionalInfo = null;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public List<string> SelectedPriorities()
        {
            List<string> selectedPriorities = new();

            foreach (var step in steps)
            {
                var answerOption = step.elements[0].answerOptions.SingleOrDefault(option => option.value.Equals("true", StringComparison.OrdinalIgnoreCase));

                if (answerOption != null)
                {
                    selectedPriorities.Add(answerOption.ResultPageLabel);
                }
            }

            return selectedPriorities;
        }
    
		internal bool GetCustomContentKeyValue(out string customKeyValue)
		{
			customKeyValue = default;
            var rtn = false;
			if (DiagnosticToolFormQuestionSixAnswer(out string yesNoAnswer))
			{
				customKeyValue = $"question6{yesNoAnswer}";
                rtn = true;
			}

			if (GetDiagnosticToolCompletedFormProductTypesListed(out IEnumerable<string> productTypesListed))
			{
				customKeyValue = $"{customKeyValue}-{string.Join("-", productTypesListed)}".TrimStart('-');
                rtn = true;
			}

			return rtn;
		}

        private bool DiagnosticToolFormQuestionSixAnswer(out string yesNoAnswer)
		{
            yesNoAnswer = default;
            if (FormType != FormTypes.DiagnosticTool || !FormIsCompleted || steps == null || !steps.Any())
            {
                return false;
            }

			var step6 = steps.SingleOrDefault(x => x.id == 6);
			yesNoAnswer = step6?.elements.FirstOrDefault()?.value;
			return !string.IsNullOrEmpty(yesNoAnswer);
		}

		private bool GetDiagnosticToolCompletedFormProductTypesListed(out IEnumerable<string> productTypesListed)
		{
            productTypesListed = null;
            if (FormType != FormTypes.DiagnosticTool || !FormIsCompleted)
            {
                return false;
            }

            if (ComparisonToolProducts == null || !ComparisonToolProducts.Any() || ProductCategories == null || !ProductCategories.Any())
            { 
                return false;
            }

            var distinctProductTypes = ComparisonToolProducts.Select(x => x.product_type).Distinct();
            productTypesListed = ProductCategories.Where(x => distinctProductTypes.Contains((long)x.id)).Select(x => x.name);
            return (bool)productTypesListed?.Any();
        }

        public string GetFormUrlName(string delimiter = "-")
        {
            return CamelCaseConverter.Delimiter(this.FormType.ToString(), delimiter).ToLower();
        }
	}
}