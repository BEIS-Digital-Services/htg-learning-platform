using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    /// <summary>
    /// A class that defines a step for a Diagnostic Tool Form.
    /// </summary>
    public class FormStep
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public FormStep()
        {
            elements = new List<FormStepElement>();
        }

        public IList<FormStepElement> elements { get; set; }

        [Required]
        public int id { get; set; }

        [Required]
        public int order { get; set; }

        public string orderClass { get; set; }

        public string orderControlHtmlId { get; set; }

        public FormDisplayControlType orderControlType { get; set; }

        public string skipConditionValue { get; set; }

        public int? skippedByElementId { get; set; }

        public int? skippedByElementStepId { get; set; }

        public bool skipStep { get; set; }

        public bool showInSummary { get; set; }

        public FormDisplayControlType titleControlType { get; set; }

        [MaxLength(150, ErrorMessage = "Form Step Title can not exceed 150 characters!")]
        public string title { get; set; }

        public string summaryTitle { get; set; }

        public string titleClass { get; set; }

        public string titleControlHtmlId { get; set; }
    }
}