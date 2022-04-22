namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    /// <summary>
    /// A class that defines an answer to a Diagnostic Tool Form Step Element.
    /// </summary>
    public class FormStepElementAnswer
    {
        /// <summary>
        /// Gets or sets an array of string containing the answers.
        /// </summary>
        public string[] Answers { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the Form Step Element.
        /// </summary>
        public int ID { get; set; }
    }
}