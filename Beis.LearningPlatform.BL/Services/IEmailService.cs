using Beis.LearningPlatform.BL.Domain;
using Beis.LearningPlatform.Library;
using System;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.BL.Services
{
    /// <summary>
    /// An interface that defines the functionality of an email service.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Gets an indication of whether the specified email address is unsubscribed.
        /// </summary>
        /// <param name="requestID">A Guid indicating the identifier of the current request.</param>
        /// <param name="emailAddress">A string containing the email address to verify.</param>
        /// <returns>A Task representing the asynchronous operation.  An IServiceResponse with payload type bool indicating the result.</returns>
        Task<IServiceResponse<bool>> IsUnsubscribed(Guid requestID, string emailAddress);

        /// <summary>
        /// Gets an indication of whether the specified email address is valid.
        /// </summary>
        /// <param name="requestID">A Guid indicating the identifier of the current request.</param>
        /// <param name="emailAddress">A string containing the email address to verify.</param>
        /// <returns>An IServiceResponse that indicates the result.</returns>
        IServiceResponse IsValidEmailAddress(Guid requestID, string emailAddress);

        /// <summary>
        /// Saves the specified Diagnostic Tool email answer.
        /// </summary>
        /// <param name="requestID">A Guid indicating the identifier of the current request.</param>
        /// <param name="diagnosticToolEmailAnswer">A DiagnosticToolEmailAnswerDto that is the email to save.</param>
        /// <returns>A Task representing the asynchronous operation.  An IServiceResponse with payload type int indicating the unique identifier of the saved email.</returns>
        Task<IServiceResponse<int>> SaveEmailAddress(Guid requestID, DiagnosticToolEmailAnswerDto diagnosticToolEmailAnswer);

        /// <summary>
        /// Sends the results email to the specified email address.
        /// </summary>
        /// <param name="requestID">A Guid indicating the identifier of the current request.</param>
        /// <param name="emailAddress">A string containing the email address to send the email to.</param>
        /// <param name="dto">An object that is the result data to email.</param>
        /// <returns>A Task representing the asynchronous operation.  An IServiceResponse indicating the result.</returns>
        Task<IServiceResponse> SendResultsRemail(Guid requestID, string emailAddress, IEmailDto dto);

        /// <summary>
        /// Unsubscribes the specified email from receiving emails.
        /// </summary>
        /// <param name="requestID">A Guid indicating the identifier of the current request.</param>
        /// <param name="emailAddress">A string containing the email address to unsubscribe.</param>
        /// <returns>A Task representing the asynchronous operation.  An IServiceResponse indicating the result.</returns>
        Task<IServiceResponse> UnsubscribeEmail(Guid requestID, string emailAddress);
    }
}