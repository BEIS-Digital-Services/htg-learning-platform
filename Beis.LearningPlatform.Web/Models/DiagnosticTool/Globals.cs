namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    /// <summary>
    /// An enumeration that defines the type of a form control.
    /// </summary>
    public enum FormDisplayControlType : int
    {
        Undefined = 0,

        Button,

        Checkbox,

        CheckboxGroup,

        DatePicker,

        Hidden,

        Label,

        Link,

        List,

        ListItem,

        Password,

        Radio,

        RadioGroup,

        Text,

        Textarea,

        TextGroup
    }

    /// <summary>
    /// An enumeration that defines search tags for a form.
    /// </summary>
    public enum FormSearchTags
    {
        Undefined = 0,

        accounting,
        accountingSoftware,
        animation,
        articles,
        analoguePractices,
        beginner,
        budget,
        caseStudy,
        checklist,
        cloudStorage,
        crm,
        crmSoftware,
        cyberSecurity,
        cyberSecuritySoftware,
        digital,
        digitalAccounting,
        digitalAdoption,
        digitalPractices,
        ecommerce,
        ecommerceSoftware,
        eligibility,
        expert,
        gdpr,
        infographic,
        inPersonTrading,
        itSupport,
        intermediate,
        leadership,
        marketing,
        onlineService,
        onlineTrading,
        planning,
        video,
        toolkit,
    }
}