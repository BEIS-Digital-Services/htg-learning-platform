namespace Beis.LearningPlatform.BL.Domain
{
    /// <summary>
    /// A class that defines the response to a service request.
    /// </summary>
    public class ServiceResponse : ResponseBase, IServiceResponse
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
            : this(requestID, isValid, default)
        { }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the unique service request identifier.</param>
        /// <param name="isValid">A bool indicating the result of the service request.</param>
        /// <param name="message">A string containing any message associated with the service request.</param>
        public ServiceResponse(Guid requestID, bool isValid, string message)
            : base(requestID)
        {
            _isValid = isValid;
            _message = message;
        }
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the unique service request identifier.</param>
        /// <param name="message">A string containing any message associated with the service request.</param>
        public ServiceResponse(Guid requestID, string message)
            : this(requestID, false, message)
        { }

        private readonly bool _isValid;
        private readonly string _message;

        bool IServiceResponse.IsValid => _isValid;

        string IServiceResponse.Message => _message;
    }
}