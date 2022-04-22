using Beis.LearningPlatform.Library.DTO;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL
{
    /// <summary>
    /// An interface that defines the functionality of an Email data service.
    /// </summary>
    public interface IFeedbackProblemReportDataService
    { 
        /// <summary>
        /// Adds a new FeedbackProblemReport record.
        /// </summary>
        /// <param name="feedbackProblemReport">A FeedbackProblemReportDto representing the feedback problem report record to add.</param>
        /// <returns>A Task representing the asynchronous operation.  An int indicating the identifier of the new item.</returns>
        Task<int> Add(FeedbackProblemReportDto feedbackProblemReport);

        /// <summary>
        /// Gets All the records.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.  An array of FeedbackPageUsefulDto containing all the results.</returns>
        Task<FeedbackProblemReportDto[]> GetAll();

        /// <summary>
        /// Updates FeedbackUseful record.
        /// </summary>
        /// <param name="feedbackProblemReport">A FeedbackProblemReportDto representing the feedback problem report record to update.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task Update(FeedbackProblemReportDto feedbackProblemReport);
    }
}