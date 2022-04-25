namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    /// <summary>
    /// A class that defines an answer to a Diagnostic Tool Form Step.
    /// </summary>
    public class FormStepAnswer
    {
        /// <summary>
        /// Gets or sets an array of string containing the answers.
        /// </summary>
        public FormStepElementAnswer[] Answers { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the Form Step.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets an indication of whether this step was skipped.
        /// </summary>
        public bool IsSkipped { get; set; }
    }
}