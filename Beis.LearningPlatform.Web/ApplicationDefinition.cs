using Beis.LearningPlatform.Web.Models.DiagnosticTool;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web
{
    public class ElementBase
    {

        public bool AdditionalInfoRequired { get; set; }
        
        public string AdditionalInfoText { get; set; }

        public FormDisplayControlType ControlType { get; set; }

        public string Hint { get; set; }
        
        public string Hint2 { get; set; }
        
        public bool ShowHintInBold { get; set; }
        
        public bool ShowHint2InBold { get; set; }

        public int Id { get; set; }

        public string Text { get; set; }
        
        public string Text2 { get; set; }
        
        public string SubText { get; set; }
        
        public string NextButtonText { get; set; }

        public string TextControlHtmlId { get; set; }

        public string TextControlHtmlName { get; set; }
        
        public int Score { get; set; }
    }

    public class OptionElement : ElementBase
    {
        public Element Parent { get; set; }

        public string ResultPageLabel { get; set; }

        public List<FormSearchTags> SearchTags { get; set; }
        
        public bool TopDownBorder { get; set; }
        
        public string Value { get; set; }
        
        public string StrongText { get; set; }
    }

    public class Element : ElementBase
    {
        public Element()
        {
            OptionElements = new List<OptionElement>();
        }

        public List<OptionElement> OptionElements { get; set; }
    }

    public class Step
    {
        public Step()
        {
            ShowInSummary = true;
            CreateElements = true;
        }

        public string SummaryTitle { get; set; }

        public List<Element> Elements { get; set; }

        public int Id { get; set; }

        public int Order { get; set; }

        public string Title { get; set; }

        public bool ShowInSummary { get; set; }

        public bool CreateElements { get; set; }

        public string SkipConditionValue { get; set; }

        public int? SkippedByElementId { get; set; }

        public int? SkippedByElementStepId { get; set; }

    }

    public class ApplicationForm
    {
        internal const string Application = "ApplicationForm";

        public ApplicationFormType DiagnosticTool { get; set; }
        public ApplicationFormType SkillsOne { get; set; }
        public ApplicationFormType SkillsTwo { get; set; }

    }
    public class ApplicationFormType
    {
        public List<Step> Steps { get; set; }

        public string SummaryHeading { get; set; }

        public bool CanChangeAnswers { get; set; }
        
        public bool BackButton { get; set; }
        
        public bool BackLink { get; set; }

        public string BackURLfromQ1 { get; set; }
    }

    public enum FormTypes
    {
        DiagnosticTool = 0,
        SkillsOne = 1,
        SkillsTwo = 2
    }
}