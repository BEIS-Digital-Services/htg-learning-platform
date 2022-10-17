using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Library.DTO.Base
{
    /// <summary>
    /// A class that defines the base properties of a DTO.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class DtoBase
    {
        /// <summary>
        /// Gets or sets the timestamp the DTO was created.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the DTO.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the session identifier of the DTO.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the URL of the DTO.
        /// </summary>
        public string Url { get; set; }
    }
}