using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Beis.LearningPlatform.DAL.Tests
{
    /// <summary>
    /// A class that defines a mock-up of a Data Service Base for testing purposes.
    /// </summary>
    internal class DataServiceBaseMock : DataServiceBase
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        internal DataServiceBaseMock(ILogger<DataServiceBase> logger, IMapper mapper)
            : base(logger, mapper)
        { }

        /// <summary>
        /// Gets the protected logger from the base class.
        /// </summary>
        internal ILogger Logger => _logger;

        /// <summary>
        /// Gets the protected mapper from the base class.
        /// </summary>
        internal IMapper Mapper => _mapper;
    }
}