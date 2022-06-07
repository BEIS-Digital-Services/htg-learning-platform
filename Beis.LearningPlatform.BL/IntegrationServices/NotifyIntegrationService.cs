namespace Beis.LearningPlatform.BL.IntegrationServices
{
    /// <summary>
    /// A class that defines an implementation of an integration service for Notify.
    /// </summary>
    public class NotifyIntegrationService : INotifyIntegrationService
    {
        private readonly ILogger _logger;
        private readonly INotifyService _notifyService;

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="options">An IOptions&lt;NotifyServiceOption&gt; that is the integration services configuration to use.</param>
        /// <param name="notifyService">An INotifyService that is the notify service to use.</param>
        public NotifyIntegrationService(ILogger<NotifyIntegrationService> logger,
            IOptions<NotifyServiceOption> options,
            INotifyService notifyService)
        {
            _logger = logger;
            _notifyService = notifyService;
            
            var option = options.Value;
            _notifyService.ConfigureService(option.BaseUrl, option.ApiKey);
        }

        async Task INotifyIntegrationService.SendDiagnosticToolResult(string emailAddress, string templateID,  Dictionary<string, dynamic> personalisation)
        {
           
            try
            {
                await _notifyService.SendTemplateEmail(emailAddress, templateID, personalisation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error whilst sending Diagnostic Tool Result email to '{emailAddress}'");
                throw new InvalidOperationException("Unable to send the Diagnostic Tool Result email", ex);
            }
        }
    }
}