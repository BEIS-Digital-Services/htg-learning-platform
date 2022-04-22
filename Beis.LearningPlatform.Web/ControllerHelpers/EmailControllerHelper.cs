using Beis.LearningPlatform.BL.Services;
using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    /// <summary>
    /// A class that defines an implementation of a helper for the Email controller.
    /// </summary>
    public class EmailControllerHelper : ControllerHelperBase, IEmailControllerHelper
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="emailService">An IEmailService that is the email service to use.</param>
        public EmailControllerHelper(ILogger<EmailControllerHelper> logger,
                                     IEmailService emailService)
            : base(logger)
        {
            _emailService = emailService;
        }

        private readonly IEmailService _emailService;

        async Task<ControllerHelperOperationResponse> IEmailControllerHelper.Unsubscribe(string emailAddress)
        {
            bool isSuccessful = false;
            string message = default;
            Guid requestID = RecordRequest();

            // Validate email address
            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                // Validate that the email address is valid
                if (_emailService.IsValidEmailAddress(requestID, emailAddress).IsValid)
                {
                    try
                    {
                        // Unsubscribe the email address
                        var result = await _emailService.UnsubscribeEmail(requestID, emailAddress);

                        if (result.IsValid)
                            isSuccessful = true;
                        else
                            throw new InvalidOperationException(result.Message ?? "Failed to unsubscribe email address");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unable to unsubscribe email address");
                        message = "We were unable to unsubscribe your email address.  Please try again";
                    }
                }
                else
                    message = "The unsubscribe email must be a valid email address";
            }
            else
                message = "An unsubscribe email address must be specified";

            return new ControllerHelperOperationResponse(requestID, isSuccessful, message);
        }
    }
}