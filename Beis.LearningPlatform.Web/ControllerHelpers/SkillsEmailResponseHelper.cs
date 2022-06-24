namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class SkillsEmailResponseHelper : IEmailResponseHelper
    {
        public FormTypes FormType => FormTypes.SkillsOne;

        public async Task<IEmailDto> ConvertToResultsEmail(DiagnosticToolForm form)
        {
            var returnValue = new SkillsResultsEmailDataDto
            {
                DigitalAdoptionBenefits = form.steps[0].elements[0].GetSelectedAnswerOptionsAsString(),
                DigitalAdoptionFrictionPointDescription = form.steps[1].elements[0].value,
                SoftwareUsage = form.steps[2].elements[0].GetSelectedAnswerOptionsAsString(),
                InformationSharingMode = form.steps[3].elements[0].GetSelectedAnswerOptionsAsString(),
                DigitalAdoptionBenefitsDescription = form.steps[4].elements[0].value
            };

            return await Task.FromResult(returnValue);
        }
    }
}