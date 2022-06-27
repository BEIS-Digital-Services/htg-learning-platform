namespace Beis.LearningPlatform.BL.Domain
{
    /// <summary>
    /// A class that implements a validation response with a payload.
    /// </summary>
    /// <typeparam name="T">The Type of the response payload.</typeparam>
    public class ServiceResponse<T> : ServiceResponse, IServiceResponse<T>
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the unique service request identifier.</param>
        public ServiceResponse(Guid requestID)
            : this(requestID, false)
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the unique service request identifier.</param>
        /// <param name="isValid">A bool indicating the result of the service request.</param>
        public ServiceResponse(Guid requestID, bool isValid)
            : this(requestID, isValid, default, default)
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the unique service request identifier.</param>
        /// <param name="isValid">A bool indicating the result of the service request.</param>
        /// <param name="message">A string containing any message associated with the service request.</param>
        /// <param name="payload">A T that is the service request response payload.</param>
        public ServiceResponse(Guid requestID, bool isValid, string message, T payload)
            : base(requestID, isValid, message)
        {
            _payload = payload;
        }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the unique service request identifier.</param>
        /// <param name="isValid">A bool indicating the result of the service request.</param>
        /// <param name="payload">A T that is the service request response payload.</param>
        public ServiceResponse(Guid requestID, bool isValid, T payload)
            : this(requestID, isValid, default, payload)
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the unique service request identifier.</param>
        /// <param name="message">A string containing any message associated with the service request.</param>
        public ServiceResponse(Guid requestID, string message)
            : this(requestID, false, message, default)
        { }

        private readonly T _payload;

        T IServiceResponse<T>.Payload => _payload;
    }
}