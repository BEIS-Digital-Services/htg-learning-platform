using Beis.LearningPlatform.Library.Enums;
using Beis.LearningPlatform.Web.ComparisonTool.Models;
using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        public SkilledModuleSubTypes SkilledModuleSubTypes { get; set; }
        
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
	}
}