using System;

namespace Beis.LearningPlatform.BL.Domain
{
    /// <summary>
    /// An interface that defines the base behaviour of a service request response.
    /// </summary>
    public interface IResponseBase
    {
        /// <summary>
        /// Gets the unique request identifier.
        /// </summary>
        Guid RequestID { get; }
    }
}