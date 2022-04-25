using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.BL.IntegrationServices.GovUkNotify
{
    /// <summary>
    /// An interface that defines the behaviour of a Notify service.
    /// </summary>
    public interface INotifyService
    {
        /// <summary>
        /// Configures the notify service.
        /// </summary>
        /// <param name="baseURL">A string containing the base URL of the Notify API.</param>
        /// <param name="apiKey">A string containing the API key for the Notify API.</param>
        void ConfigureService(string baseURL, string apiKey);

        /// <summary>
        /// Sends the specified template email to the specified email address.
        /// </summary>
        /// <param name="emailAddress">A string containing the email address to send the email to.</param>
        /// <param name="templateId">A string containing the identifier of the template.</param>
        /// <param name="personalisation">A Dictionary of key string and value dynamic containing any personalisation data for the email template.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task SendTemplateEmail(string emailAddress, string templateId, Dictionary<string, dynamic> personalisation = default);

        /// <summary>
        /// Sends the specified template email to the specified email addresses.
        /// </summary>
        /// <param name="emailAddresses">An array of string containing the email addresses to send the email to.</param>
        /// <param name="templateId">A string containing the identifier of the template.</param>
        /// <param name="personalisation">A Dictionary of key string and value dynamic containing any personalisation data for the email template.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task SendTemplateEmail(string[] emailAddresses, string templateId, Dictionary<string, dynamic> personalisation = default);

        /// <summary>
        /// Gets an indication of whether the Notify service has been configured.
        /// </summary>
        bool IsConfigured { get; }
    }
}