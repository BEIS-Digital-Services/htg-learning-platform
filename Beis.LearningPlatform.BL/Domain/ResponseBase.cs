using System;

namespace Beis.LearningPlatform.BL.Domain
{
    /// <summary>
    /// A class that defines the base functionality of a response.
    /// </summary>
    public class ResponseBase : IResponseBase
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="requestID">A Guid containing the unique service request identifier.</param>
        public ResponseBase(Guid requestID)
        {
            _requestID = requestID;
        }

        private readonly Guid _requestID;

        Guid IResponseBase.RequestID => _requestID;
    }
}