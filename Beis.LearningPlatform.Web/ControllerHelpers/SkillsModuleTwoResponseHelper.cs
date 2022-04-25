using Beis.LearningPlatform.Library;
using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Models.DiagnosticTool;
using Beis.LearningPlatform.Web.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class SkillsModuleTwoResponseHelper : IEmailResponseHelper
    {
        public FormTypes FormType => FormTypes.SkillsTwo;

        public async Task<IEmailDto> ConvertToResultsEmail(DiagnosticToolForm form)
        {

            var selectedPriorities = form.SelectedPriorities();
            var returnValue = new SkilledModuleTwoDto
            {
                Priorities = selectedPriorities.Any() ? selectedPriorities.ToArray().JoinToSeparatedList() : "You have not selected any business priorities",
                SkilledModuleTwoResultType = form.SkilledModuleTwoResultType                
            };

            return await Task.FromResult(returnValue);
        }
    }
}