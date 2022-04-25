using Beis.LearningPlatform.Data.Entities.Feedback;
using Beis.LearningPlatform.Data.Repositories.Base;

namespace Beis.LearningPlatform.Data.Repositories.Feedback
{
    public   class FeedbackProblemReportRepository : FeedbackGenericRepository<FeedbackProblemReport>, IFeedbackProblemReportRepository
    {
        public FeedbackProblemReportRepository(DataContext context) : base(context)
        {
        }
    }
}