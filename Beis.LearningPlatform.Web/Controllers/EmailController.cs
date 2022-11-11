namespace Beis.LearningPlatform.Web.Controllers
{
    /// <summary>
    /// A class that defines a controller for Email actions.
    /// </summary>
    public class EmailController : ControllerBase
    {
        private readonly IEmailControllerHelper _controllerHelper;

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        public EmailController(ILogger<EmailController> logger, IEmailControllerHelper controllerHelper)
            : base(logger)
        {
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        [Route("/email/unsubscribe")]
        public async Task<IActionResult> Unsubscribe([FromQuery] string emailAddress)
        {
            _logger.LogTrace($"Unsubscribe email request: {emailAddress}");

            try
            {
                await _controllerHelper.Unsubscribe(emailAddress);
            }
            catch (ArgumentNullException ex)
            { 
                _logger.LogDebug($"Unsubscribe email {emailAddress} error.", ex);
            }
            catch (InvalidDataException ex)
            { 
                _logger.LogDebug($"Unsubscribe email {emailAddress} error.", ex);
            }
            catch (InvalidOperationException ex)
            { 
                _logger.LogWarning($"Unsubscribe email {emailAddress} error.", ex);
            }
            catch (Exception ex)
            { 
                _logger.LogError($"Unsubscribe email {emailAddress} error.", ex);
            }
            
            return View("Unsubscribe", new PageViewModel
            {
                pageTitle = "Help to Grow: Digital - Unsubscribe Email",
                pagename = "Unsubscribe Email",
            });

        }
    }
}