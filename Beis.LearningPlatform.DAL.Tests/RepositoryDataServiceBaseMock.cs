using AutoMapper;
using Beis.LearningPlatform.Data.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL.Tests
{
    /// <summary>
    /// A class that defines a mock-up of a Repository Data Service Base for testing purposes.
    /// </summary>
    internal class RepositoryDataServiceBaseMock : RepositoryDataServiceBase<IFakeRepository>
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="dataRepository">An IDataRepository that is the Unit of Work data repository to use.</param>
        /// <param name="repository">A FakeRepository that is the repository to use.</param>
        internal RepositoryDataServiceBaseMock(ILogger<DataServiceBase> logger, IMapper mapper, IDataRepository dataRepository, IFakeRepository repository)
            : base(logger, mapper, dataRepository, repository)
        { }

        /// <summary>
        /// Invokes the protected Save method in the base class.
        /// </summary>
        /// <returns>The return value from the base class method.</returns>
        internal int SaveMock() => Save();

        /// <summary>
        /// Invokes the protected SaveAsync method in the base class.
        /// </summary>
        /// <returns>The return value from the base class method.</returns>
        internal Task<int> SaveAsyncMock() => SaveAsync();

        /// <summary>
        /// Gets the protected data repository from the base class.
        /// </summary>
        internal IDataRepository DataRepository => _dataRepository;

        /// <summary>
        /// Gets the protected logger from the base class.
        /// </summary>
        internal ILogger Logger => _logger;

        /// <summary>
        /// Gets the protected mapper from the base class.
        /// </summary>
        internal IMapper Mapper => _mapper;

        /// <summary>
        /// Gets the protected repository from the base class.
        /// </summary>
        internal IFakeRepository Repository => _repository;
    }
}