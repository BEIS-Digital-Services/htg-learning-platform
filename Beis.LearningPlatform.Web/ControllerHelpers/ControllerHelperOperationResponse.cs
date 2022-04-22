using System;

namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    /// <summary>
    /// A class that defines a response from a controller helper operation.
    /// </summary>
    public class ControllerHelperOperationResponse
    {

        public ControllerHelperOperationResponse(Guid requestID)
            : this(requestID, true)
        { }

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the source request identifier.</param>
        /// <param name="result">A bool that indicates the result of the operation.</param>
        public ControllerHelperOperationResponse(Guid requestID, bool result)
            : this(requestID, result, Array.Empty<string>())
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the source request identifier.</param>
        /// <param name="result">A bool that indicates the result of the operation.</param>
        /// <param name="message">A string containing any message associated with the operation.</param>
        public ControllerHelperOperationResponse(Guid requestID, bool result, string message)
            : this(requestID, result, string.IsNullOrWhiteSpace(message) ? Array.Empty<string>() : new[] { message })
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the source request identifier.</param>
        /// <param name="result">A bool that indicates the result of the operation.</param>
        /// <param name="messages">An array of string containing any messages associated with the operation.</param>
        public ControllerHelperOperationResponse(Guid requestID, bool result, string[] messages)
        {
            RequestID = requestID;
            Result = result;
            Messages = messages;
        }

        /// <summary>
        /// Gets the first of any messages associated with the response.
        /// </summary>
        public string Message { get => Messages?[0]; }

        /// <summary>
        /// Gets any messages associated with the response. 
        /// </summary>
        public string[] Messages { get; }

        /// <summary>
        /// Gets the source request identifier.
        /// </summary>
        public Guid RequestID { get; }

        /// <summary>
        /// Gets the result of the operation.
        /// </summary>
        public bool Result { get; }

        public string ErrorHeading { get; set; }
    }
}