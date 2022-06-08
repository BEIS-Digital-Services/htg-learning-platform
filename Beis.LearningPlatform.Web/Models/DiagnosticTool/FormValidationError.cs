namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    /// <summary>
    /// A class that defines a validation error for a Diagnostic Tool Form.
    /// </summary>
    public class FormValidationError
    {
        public string displayControlClass { get; set; }
        public string errorHeading { get; set; }
        public string errorMessage { get; set; }

        public string htmlId { get; set; }

        [Required]
        public int id { get; set; }
    }
}