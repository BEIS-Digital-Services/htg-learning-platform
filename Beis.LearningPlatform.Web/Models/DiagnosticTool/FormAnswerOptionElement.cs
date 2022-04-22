using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{

    /// <summary>
    /// A class that defines a answer option element for a Diagnostic Tool Form.
    /// </summary>
    public class FormAnswerOptionElement : StepElementBase
    {
        public FormStepElement parent { get; set; }

        public string ResultPageLabel { get; set; }

        public IList<FormSearchTags> searchTags { get; set; }
        public bool topDownBorder { get; set; }
    }
}