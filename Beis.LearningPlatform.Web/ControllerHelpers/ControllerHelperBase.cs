using System.Runtime.CompilerServices;

namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    /// <summary>
    /// A class that defines the base functionality of a controller helper.
    /// </summary>
    public class ControllerHelperBase : IControllerHelperBase
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters,
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        protected ControllerHelperBase(ILogger<ControllerHelperBase> logger)
        {
            _logger = logger;

            _controllerHelperType = GetType().Name;
            _logger.LogInformation($"ControllerHelper \"{_controllerHelperType}\" created");
        }

        /// <summary>
        /// The client service.
        /// </summary>
        private readonly string _controllerHelperType;
        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Generates a request identifier and records a received request in the logs.
        /// </summary>
        /// <param name="requestMethodName">A string containing the name of the request method.</param>
        /// <returns>A Guid containing the unique identifier for the request.</returns>
        protected Guid RecordRequest([CallerMemberName] string requestMethodName = null)
        {
            var returnValue = Guid.NewGuid();
            _logger.LogInformation($"Request {returnValue} for {_controllerHelperType}.{requestMethodName} received");
            return returnValue;
        }
    }
}