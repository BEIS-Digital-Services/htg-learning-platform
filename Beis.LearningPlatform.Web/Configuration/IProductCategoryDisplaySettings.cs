using Beis.LearningPlatform.Web.StrapiApi.Models;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Configuration
{
    public interface IProductCategoryDisplaySettings
    {
        IList<CMSSearchTag> DisplaySettings { get; }
        bool? ShowAllProductStatuses { get; }
    }
}