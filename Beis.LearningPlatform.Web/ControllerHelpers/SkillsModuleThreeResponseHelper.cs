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
        public FormTypes FormType => FormTypes.SkillsThreeNewcomerCommunication | FormTypes.SkillsThreeNewcomerPlanning;

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

                QuestionThreeStart = form.steps[3].elements[0].answerOptions[0].value,
                QuestionThreeNext = form.steps[3].elements[0].answerOptions[1].value,
                QuestionThreeFinally = form.steps[3].elements[0].answerOptions[2].value,
                SkilledModuleTwoResultType = form.SkilledModuleTwoResultType,
                SkilledModuleSubTypes = form.SkilledModuleSubTypes,
            };

            return await Task.FromResult(returnValue);
        }
    }
}