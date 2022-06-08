using Beis.LearningPlatform.Data.Repositories.Base;

namespace Beis.LearningPlatform.DAL
{
    /// <summary>
    /// A class that defines the base functionality of a repository-based data service.
    /// </summary>
    /// <typeparam name="R">The Type of the repository.  Must inherit from IRepositoryBase.</typeparam>
    public abstract class RepositoryDataServiceBase<R> : DataServiceBase
        where R : IRepositoryBase
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="dataRepository">An IDataRepository that is the Unit of Work data repository to use.</param>
        /// <param name="repository">A R that is the repository to use.</param>
        protected RepositoryDataServiceBase(ILogger<DataServiceBase> logger, IMapper mapper, IDataRepository dataRepository, R repository)
            : base(logger, mapper)
        {
            _dataRepository = dataRepository;
            _repository = repository;
        }

        /// <summary>
        /// The data repository to use for Unit of Work transactions.
        /// </summary>
        protected readonly IDataRepository _dataRepository;
        /// <summary>
        /// The repository to use.
        /// </summary>
        protected readonly R _repository;

        /// <summary>
        /// Saves the changes made to the repository.
        /// </summary>
        /// <returns>An int indicating the number of state entries written to the data store.</returns>
        protected int Save()
        {
            return _dataRepository.Save();
        }

        /// <summary>
        /// Asynchronously saves the changes made to the repository.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.  An int indicating the number of state entries written to the data store.</returns>
        async protected Task<int> SaveAsync()
        {
            return await _dataRepository.SaveAsync();
        }
    }
}