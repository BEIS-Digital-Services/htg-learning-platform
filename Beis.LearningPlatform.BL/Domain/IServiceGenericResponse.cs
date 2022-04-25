namespace Beis.LearningPlatform.BL.Domain
{
    /// <summary>
    /// An interface that defines a service response with a payload.
    /// </summary>
    /// <typeparam name="T">The Type of the response payload.</typeparam>
    public interface IServiceResponse<T> : IServiceResponse
    {
        /// <summary>
        /// Gets the payload resulting from the service request.
        /// </summary>
        T Payload { get; }
    }
}