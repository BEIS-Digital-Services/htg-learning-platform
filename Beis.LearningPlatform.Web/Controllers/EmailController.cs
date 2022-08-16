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
            _logger.LogTrace("Unsubscribe email: {emailAddress}", emailAddress);

            await _controllerHelper.Unsubscribe(emailAddress);
            
            return View("Unsubscribe", new PageViewModel
            {
                pageTitle = "Help to Grow: Digital - Unsubscribe Email",
                pagename = "Unsubscribe Email",
            });

        }
    }
}