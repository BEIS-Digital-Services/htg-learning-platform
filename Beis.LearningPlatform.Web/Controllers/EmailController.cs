using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Controllers
{
    /// <summary>
    /// A class that defines a controller for Email actions.
    /// </summary>
    public class EmailController : ControllerBase
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        public EmailController(ILogger<EmailController> logger,
                               IEmailControllerHelper controllerHelper)
            : base(logger)
        {
            _controllerHelper = controllerHelper;
        }

        private readonly IEmailControllerHelper _controllerHelper;

        [HttpGet]
        [Route("/email/unsubscribe")]
        public async Task<IActionResult> Unsubscribe([FromQuery] string emailAddress)
        {
            _logger.LogTrace($"Unsubscribe email: {emailAddress}");

            var result = await _controllerHelper.Unsubscribe(emailAddress);
            
            if (!result.Result)
            {
                _logger.LogWarning($"Unsubscried email failed with message: {result.Message}");
                return ReturnErrorPage(result.RequestID, result.Message);
            }

            return View("Unsubscribe", new PageViewModel
            {
                pageTitle = "Help to Grow: Digital - Unsubscribe Email",
                pagename = "Unsubscribe Email",
            });

        }
    }
}