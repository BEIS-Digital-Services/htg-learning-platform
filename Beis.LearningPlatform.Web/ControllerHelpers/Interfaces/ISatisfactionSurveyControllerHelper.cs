using Beis.LearningPlatform.Web.Models;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface ISatisfactionSurveyControllerHelper
    {
        Task<ControllerHelperOperationResponse> SaveSatisfactionSurvey(SatisfactionSurveyViewModel surveyForm);
    }
}