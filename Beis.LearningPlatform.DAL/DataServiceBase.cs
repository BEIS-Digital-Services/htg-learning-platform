namespace Beis.LearningPlatform.DAL
{
    /// <summary>
    /// A class that defines the base functionality of a data service.
    /// </summary>
    public abstract class DataServiceBase
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        protected DataServiceBase(ILogger<DataServiceBase> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// The logger to use.
        /// </summary>
        protected readonly ILogger _logger;
        /// <summary>
        /// The mapper to use.
        /// </summary>
        protected readonly IMapper _mapper;
    }
}