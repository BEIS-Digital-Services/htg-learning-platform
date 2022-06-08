namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    /// <summary>
    /// An interface that defines the behaviour of a helper for the Email controller.
    /// </summary>
    public interface IEmailControllerHelper : IControllerHelperBase
    {
        /// <summary>
        /// Unsubscribes the specified email address.
        /// </summary>
        /// <param name="emailAddress">A string containing the email address to unsubscribe.</param>
        /// <returns>A Task representing the asynchronous operation.  A ControllerHelperOperationResponse containing the result.</returns>
        Task<ControllerHelperOperationResponse> Unsubscribe(string emailAddress);
    }
}