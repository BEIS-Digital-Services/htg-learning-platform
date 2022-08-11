namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class EmailControllerHelper : ControllerHelperBase, IEmailControllerHelper
    {
        private readonly IEmailService _emailService;
     
        public EmailControllerHelper(ILogger<EmailControllerHelper> logger, IEmailService emailService) : base(logger)
        {
            _emailService = emailService;
        }

        public async Task Unsubscribe(string emailAddress)
        {
            var requestId = RecordRequest();
            
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentNullException(nameof(emailAddress), "An unsubscribe email address must be specified");

            if (!_emailService.IsValidEmailAddress(requestId, emailAddress).IsValid)
                throw new InvalidDataException("The unsubscribe email must be a valid email address");

            try
            {
                var serviceResult = await _emailService.UnsubscribeEmail(requestId, emailAddress);
                if (!serviceResult.IsValid)
                    throw new InvalidOperationException(serviceResult.Message ?? "Failed to unsubscribe email address");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to unsubscribe email address");
                throw new Exception("We were unable to unsubscribe your email address.  Please try again", ex);
            }
        }
    }
}