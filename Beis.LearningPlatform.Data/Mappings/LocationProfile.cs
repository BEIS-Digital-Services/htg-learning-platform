using AutoMapper;
using Beis.LearningPlatform.Data.Entities.Locations;
using Beis.LearningPlatform.Data.Models;

namespace Beis.LearningPlatform.Data.Mappings
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationModel>();
            CreateMap<LocationModel, Location>();
        }
    }
}