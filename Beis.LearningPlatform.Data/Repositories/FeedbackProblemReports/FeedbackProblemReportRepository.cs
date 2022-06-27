namespace Beis.LearningPlatform.Data.Repositories.Feedback
{
    public   class FeedbackProblemReportRepository : FeedbackGenericRepository<FeedbackProblemReport>, IFeedbackProblemReportRepository
    {
        public FeedbackProblemReportRepository(DataContext context) : base(context)
        {
        }
    }
}