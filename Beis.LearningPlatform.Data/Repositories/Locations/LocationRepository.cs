using Beis.LearningPlatform.Data.Entities.Locations;
using Beis.LearningPlatform.Data.Repositories.Base;

namespace Beis.LearningPlatform.Data.Repositories.Locations
{
    public   class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(DataContext context) : base(context)
        {
        }
    }
}