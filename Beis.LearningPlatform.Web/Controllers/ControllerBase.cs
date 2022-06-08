namespace Beis.LearningPlatform.Web.Controllers
{
    /// <summary>
    /// A class that defines the base functionality of a controller.
    /// </summary>
    public class ControllerBase : Controller, IController
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        public ControllerBase(ILogger<ControllerBase> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The logger to use.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Returns the error page view with the specified error details.
        /// </summary>
        /// <param name="requestID">A Guid indicating the unique identifier of the request.</param>
        /// <param name="errorMessage">A string containing the error message.</param>
        /// <param name="sessionID">A string containing the session identifier.</param>
        /// <returns>A ViewResult representing the error page view.</returns>
        public ViewResult ReturnErrorPage(Guid requestID, string errorMessage, string sessionID = default)
        {            
            ErrorViewModel error = new()
            {
                ErrorMessage = errorMessage,
                RequestId = requestID.ToString(),
                SessionId = sessionID
            };

            return View("Error", error);
        }

        protected JsonResult JsonBadRequest()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(Response.StatusCode);
        }

    }
}