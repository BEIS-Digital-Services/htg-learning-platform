using Beis.LearningPlatform.Library;
using Beis.LearningPlatform.Web.Models.DiagnosticTool;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface IEmailResponseHelper
    {
        Task<IEmailDto> ConvertToResultsEmail(DiagnosticToolForm form);

        FormTypes FormType { get; }
    }    
}