namespace Beis.LearningPlatform.Data.Repositories.DiagnosticTool
{
    /// <summary>
    /// A class that defines an implementation of a Diagnostic Tool Email Answer repository.
    /// </summary>
    public class DiagnosticToolEmailAnswerRepository : FeedbackGenericRepository<DiagnosticToolEmailAnswer>, IDiagnosticToolEmailAnswerRepository
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="context">A DataContext that is the data context to use.</param>
        public DiagnosticToolEmailAnswerRepository(DataContext context)
            : base(context)
        { }
    }
}