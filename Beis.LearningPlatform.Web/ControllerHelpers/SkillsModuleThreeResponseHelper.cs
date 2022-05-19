using Beis.LearningPlatform.Library;
using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Models.DiagnosticTool;
using Beis.LearningPlatform.Web.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class SkillsModuleThreeResponseHelper : IEmailResponseHelper
    {
        public FormTypes FormType => FormTypes.SkillsThreeNewcomerPlanning | FormTypes.SkillsThreeNewcomerCommunication | FormTypes.SkillsThreeNewcomerSupport |
                                        FormTypes.SkillsThreeNewcomerTraining | FormTypes.SkillsThreeNewcomerTesting | FormTypes.SkillsThreeMoverPlanning |
                                        FormTypes.SkillsThreeMoverCommunication | FormTypes.SkillsThreeMoverSupport | FormTypes.SkillsThreeMoverTraining |
                                        FormTypes.SkillsThreeMoverTesting | FormTypes.SkillsThreePerformerPlanning | FormTypes.SkillsThreePerformerCommunication |
                                        FormTypes.SkillsThreePerformerSupport | FormTypes.SkillsThreePerformerTraining | FormTypes.SkillsThreePerformerTesting ;

        public async Task<IEmailDto> ConvertToResultsEmail(DiagnosticToolForm form)
        {
            
            var returnValue = new SkilledModuleThreeDto
            {
                QuestionOneStart = form.steps[0].elements[0].answerOptions[0].value,
                QuestionOneNext = form.steps[0].elements[0].answerOptions[1].value,
                QuestionOneFinally = form.steps[0].elements[0].answerOptions[2].value,

                QuestionTwoStart = form.steps[1].elements[0].answerOptions[0].value,
                QuestionTwoNext = form.steps[1].elements[0].answerOptions[1].value,
                QuestionTwoFinally = form.steps[1].elements[0].answerOptions[2].value,

                QuestionThreeStart = form.steps[2].elements[0].answerOptions[0].value,
                QuestionThreeNext = form.steps[2].elements[0].answerOptions[1].value,
                QuestionThreeFinally = form.steps[2].elements[0].answerOptions[2].value,
                UserTypeActionPlanSection = form.userTypeActionPlanSection
            };

            return await Task.FromResult(returnValue);
        }
    }
}