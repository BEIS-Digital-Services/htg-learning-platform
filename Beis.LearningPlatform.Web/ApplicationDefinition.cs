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
        public ApplicationFormType SkillsThreeNewcomerPlanning { get; set; }
        public ApplicationFormType SkillsThreeNewcomerCommunication { get; set; }
        public ApplicationFormType SkillsThreeNewcomerSupport { get; set; }
        public ApplicationFormType SkillsThreeNewcomerTraining { get; set; }
        public ApplicationFormType SkillsThreeNewcomerTesting { get; set; }

        public ApplicationFormType SkillsThreeMoverPlanning { get; set; }
        public ApplicationFormType SkillsThreeMoverCommunication { get; set; }
        public ApplicationFormType SkillsThreeMoverSupport { get; set; }
        public ApplicationFormType SkillsThreeMoverTraining { get; set; }
        public ApplicationFormType SkillsThreeMoverTesting { get; set; }

        public ApplicationFormType SkillsThreePerformerPlanning { get; set; }
        public ApplicationFormType SkillsThreePerformerCommunication { get; set; }
        public ApplicationFormType SkillsThreePerformerSupport { get; set; }
        public ApplicationFormType SkillsThreePerformerTraining { get; set; }
        public ApplicationFormType SkillsThreePerformerTesting { get; set; }


    }
    public class ApplicationFormType
    {
        public List<Step> Steps { get; set; }

        public string SummaryHeading { get; set; }

        public bool CanChangeAnswers { get; set; }
        
        public bool BackButton { get; set; }
        
        public bool BackLink { get; set; }

        public string BackURLfromQ1 { get; set; }
        public string UserTypeActionPlanSection { get; set; }
    }

    public enum FormTypes
    {
        DiagnosticTool = 0,
        SkillsOne = 1,
        SkillsTwo = 2,

        SkillsThreeNewcomerPlanning = 3,
        SkillsThreeNewcomerCommunication = 4,
        SkillsThreeNewcomerSupport = 5,
        SkillsThreeNewcomerTraining = 6, 
        SkillsThreeNewcomerTesting = 7, 

        SkillsThreeMoverPlanning = 8, 
        SkillsThreeMoverCommunication = 9, 
        SkillsThreeMoverSupport = 10, 
        SkillsThreeMoverTraining = 11, 
        SkillsThreeMoverTesting = 12, 

        SkillsThreePerformerPlanning = 13, 
        SkillsThreePerformerCommunication = 14, 
        SkillsThreePerformerSupport = 15, 
        SkillsThreePerformerTraining = 16, 
        SkillsThreePerformerTesting = 17 
    }
}