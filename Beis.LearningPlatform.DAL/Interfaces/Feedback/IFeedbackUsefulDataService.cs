namespace Beis.LearningPlatform.DAL
{
    /// <summary>
    /// An interface that defines the functionality of an Email data service.
    /// </summary>
    public interface IFeedbackUsefulDataService
    {
        /// <summary>
        /// Adds a new FeedbackUseful record.
        /// </summary>
        /// <param name="feedbackUsefulAnswer">A FeedbackPageUsefulDto representing the feedback record to add.</param>
        /// <returns>A Task representing the asynchronous operation.  An int indicating the identifier of the new item.</returns>
        Task<int> Add(FeedbackPageUsefulDto feedbackUsefulAnswer);

        /// <summary>
        /// Gets All the records.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.  An array of FeedbackPageUsefulDto containing all the results.</returns>
        Task<FeedbackPageUsefulDto[]> GetAll();

        /// <summary>
        /// Updates FeedbackUseful record.
        /// </summary>
        /// <param name="feedbackUsefulAnswer">A FeedbackPageUsefulDto representing the feedback record to update.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task Update(FeedbackPageUsefulDto feedbackUsefulAnswer);
    }
}