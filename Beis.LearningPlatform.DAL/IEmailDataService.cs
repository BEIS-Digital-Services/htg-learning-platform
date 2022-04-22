using Beis.LearningPlatform.Library;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL
{
    /// <summary>
    /// An interface that defines the functionality of an Email data service.
    /// </summary>
    public interface IEmailDataService
    {
        /// <summary>
        /// Adds an Diagnostic Tool Email Answer.
        /// </summary>
        /// <param name="diagnosticToolEmailAnswer">A DiagnosticToolEmailAnswerDto representing the item to add.</param>
        /// <returns>A Task representing the asynchronous operation.  An int indicating the identifier of the new item.</returns>
        Task<int> Add(DiagnosticToolEmailAnswerDto diagnosticToolEmailAnswer);

        /// <summary>
        /// Gets Diagnostic Tool Email Answers that match the specified email address.
        /// </summary>
        /// <param name="emailAddress">A string containing the email address to get the results for.</param>
        /// <returns>A Task representing the asynchronous operation.  An array of DiagnosticToolEmailAnswerDto containing the matching results.</returns>
        Task<DiagnosticToolEmailAnswerDto[]> GetByEmail(string emailAddress);

        /// <summary>
        /// Updates a Diagnostic Tool Email Answer.
        /// </summary>
        /// <param name="diagnosticToolEmailAnswer">A DiagnosticToolEmailAnswerDto representing the item to update.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task Update(DiagnosticToolEmailAnswerDto diagnosticToolEmailAnswer);
    }
}