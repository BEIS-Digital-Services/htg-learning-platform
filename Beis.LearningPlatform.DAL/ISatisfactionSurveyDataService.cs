using Beis.LearningPlatform.Library;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL
{
    public interface ISatisfactionSurveyDataService
    {
        Task<int> Add(SatisfactionSurveyDto satisfactionSurveyDto);
    }
}