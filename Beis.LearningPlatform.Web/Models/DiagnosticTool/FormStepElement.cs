using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    /// <summary>
    /// A class that defines a step element for a Diagnostic Tool Form.
    /// </summary>
    public class FormStepElement : StepElementBase
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public FormStepElement()
        {
            answerOptions = new List<FormAnswerOptionElement>();
        }

        public IList<FormAnswerOptionElement> answerOptions { get; set; }

        public bool childHasErrors { get; set; }

        public int? skippedByElementId { get; set; }
    }
}