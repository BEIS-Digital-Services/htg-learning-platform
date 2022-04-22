namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    public class ElementInfo
    {
        public int CurrentStep { get; set; }
        public int CurrentElement { get; set; }
        public int RadioButtonParent { get; set; }
        public FormStepElement CurrentFormStepElement { get; set; }
        public int ChildIndex { get; set; }
        public FormStep CurrentFormStep { get; set; }
    }
}