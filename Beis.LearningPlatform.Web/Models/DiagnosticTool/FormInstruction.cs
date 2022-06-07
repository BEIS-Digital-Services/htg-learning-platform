namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    /// <summary>
    /// A class that defines an instruction for a Diagnostic Tool Form.
    /// </summary>
    public class FormInstruction
    {
        public string displayClass { get; set; }

        public string htmlId { get; set; }

        [Required]
        public int id { get; set; }

        [Required]
        public string text { get; set; }
    }
}