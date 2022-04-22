namespace Beis.LearningPlatform.BL.Domain
{
    /// <summary>
    /// An interface that defines the response to a service request.
    /// </summary>
    public interface IServiceResponse : IResponseBase
    {
        /// <summary>
        /// Gets an indication of whether the service request was valid.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Gets any message associated with the service request.
        /// </summary>
        string Message { get; }
    }
}