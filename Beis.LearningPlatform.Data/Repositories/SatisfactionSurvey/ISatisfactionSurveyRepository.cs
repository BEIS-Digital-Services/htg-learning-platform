using Beis.LearningPlatform.Data.Entities.SatisfactionSurvey;
using Beis.LearningPlatform.Data.Repositories.Base;

namespace Beis.LearningPlatform.Data.Repositories.SatisfactionSurvey
{
    public interface ISatisfactionSurveyRepository : IFeedbackGenericRepository<SatisfactionSurveyEntry>
    {
    }
}