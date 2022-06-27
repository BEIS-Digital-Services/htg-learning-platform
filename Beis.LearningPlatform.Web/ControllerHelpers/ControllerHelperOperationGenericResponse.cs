namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    /// <summary>
    /// A class that defines a response with a payload from a controller helper operation.
    /// </summary>
    /// <typeparam name="T">The Type of the response payload.</typeparam>
    public class ControllerHelperOperationResponse<T> : ControllerHelperOperationResponse
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the source request identifier.</param>
        /// <param name="result">A bool that indicates the result of the operation.</param>
        public ControllerHelperOperationResponse(Guid requestID, bool result)
            : this(requestID, result, default(T))
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the source request identifier.</param>
        /// <param name="result">A bool that indicates the result of the operation.</param>
        /// <param name="message">A string containing any message associated with the operation.</param>
        public ControllerHelperOperationResponse(Guid requestID, bool result, string message)
            : this(requestID, result, string.IsNullOrWhiteSpace(message) ? Array.Empty<string>() : new[] { message }, default)
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the source request identifier.</param>
        /// <param name="result">A bool that indicates the result of the operation.</param>
        /// <param name="message">A string containing any message associated with the operation.</param>
        /// <param name="payload">A T containing the response payload.</param>
        public ControllerHelperOperationResponse(Guid requestID, bool result, string message, T payload)
            : this(requestID, result, string.IsNullOrWhiteSpace(message) ? Array.Empty<string>() : new[] { message }, payload)
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the source request identifier.</param>
        /// <param name="result">A bool that indicates the result of the operation.</param>
        /// <param name="messages">An array of string containing any messages associated with the operation.</param>
        public ControllerHelperOperationResponse(Guid requestID, bool result, string[] messages)
            : this(requestID, result, messages, default)
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the source request identifier.</param>
        /// <param name="result">A bool that indicates the result of the operation.</param>
        /// <param name="messages">An array of string containing any messages associated with the operation.</param>
        /// <param name="payload">A T containing the response payload.</param>
        public ControllerHelperOperationResponse(Guid requestID, bool result, string[] messages, T payload)
            : base(requestID, result, messages)
        {
            Payload = payload;
        }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the source request identifier.</param>
        /// <param name="result">A bool that indicates the result of the operation.</param>
        /// <param name="payload">A T containing the response payload.</param>
        public ControllerHelperOperationResponse(Guid requestID, bool result, T payload)
            : this(requestID, result, Array.Empty<string>(), payload)
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the source request identifier.</param>
        /// <param name="payload">A T containing the response payload.</param>
        public ControllerHelperOperationResponse(Guid requestID, T payload)
            : this(requestID, true, payload)
        { }

        public ControllerHelperOperationResponse(Guid requestId)
            : this(requestId, true)
        { }

        /// <summary>
        /// Gets the response payload.
        /// </summary>
        public T Payload { get; }
    }
}