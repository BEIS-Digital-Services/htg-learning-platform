using Microsoft.Extensions.Logging;
using Notify.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.BL.IntegrationServices.GovUkNotify
{
    /// <summary>
    /// A class that defines an implementation of a Notify service.
    /// </summary>
    public class NotifyService : INotifyService
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        public NotifyService(ILogger<NotifyService> logger)
        {
            _logger = logger;

            _isConfigured = false;
            _notifyServiceInterface = this;
        }

        private string _apiKey;
        private string _baseURL;
        private bool _isConfigured;
        private readonly ILogger _logger;
        private readonly INotifyService _notifyServiceInterface;

        void INotifyService.ConfigureService(string baseURL, string apiKey)
        {
            if (!_isConfigured)
            {
                _baseURL = baseURL;
                _apiKey = apiKey;
                _isConfigured = true;

                _logger.LogInformation("Configured Notify Service");
            }
            else
                throw new InvalidOperationException("The Notify Service has already been configured");
        }

        async Task INotifyService.SendTemplateEmail(string emailAddress, string templateId, Dictionary<string, dynamic> personalisation)
        {
            if (!string.IsNullOrWhiteSpace(emailAddress))
                await _notifyServiceInterface.SendTemplateEmail(new[] { emailAddress }, templateId, personalisation);
            else
                throw new ArgumentNullException(nameof(emailAddress));
        }

        async Task INotifyService.SendTemplateEmail(string[] emailAddresses, string templateId, Dictionary<string, dynamic> personalisation)
        {
            if (emailAddresses?.Length > 0)
            {
                var client = new NotificationClient(_baseURL, _apiKey);
                foreach (var emailAddress in emailAddresses)
                {
                    _logger.LogInformation($"Sending email template '{templateId}' to '{emailAddress}'");

                    var sendEmailResult = await client.SendEmailAsync(emailAddress, templateId, personalisation);
                }
            }
            else
                throw new ArgumentNullException(nameof(emailAddresses));
        }

        bool INotifyService.IsConfigured => _isConfigured;
    }
}