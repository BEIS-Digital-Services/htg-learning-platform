using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.BL.IntegrationServices
{
    /// <summary>
    /// An interface that defines the behaviour of an integration service for Notify.
    /// </summary>
    public interface INotifyIntegrationService
    {
        /// <summary>
        /// Emails the results of the Diagnostic Tool.
        /// </summary>
        /// <param name="emailAddress">A string containing the email address of the recipient.</param>
        /// <param name="templateID">Id of the email template.</param>
        /// <param name="personalisation">Data for the email.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task SendDiagnosticToolResult(string emailAddress, string templateID, Dictionary<string, dynamic> personalisation);

    }
}