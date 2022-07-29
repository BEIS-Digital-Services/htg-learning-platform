namespace Beis.LearningPlatform.Data.Repositories
{
    /// <summary>
    /// Data Repository (Unit Of Work)
    /// </summary>
    public class DataRepository : IDataRepository
    {
        private readonly DataContext context;

        public IFeedbackProblemReportRepository FeedbackProblemReports { get; }

        public IFeedbackPageUsefulRepository FeedbackPageUsefuls { get; }

        public IDiagnosticToolEmailAnswerRepository DiagnosticToolEmailAnswers { get; }

        public DataRepository(DataContext context
            , IFeedbackProblemReportRepository feedbackProblemReportRepository
            , IFeedbackPageUsefulRepository feedbackPageUsefulRepository
            )
        {
            this.context = context;
            FeedbackProblemReports = feedbackProblemReportRepository;
            FeedbackPageUsefuls = feedbackPageUsefulRepository;
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();
        }
    }
}