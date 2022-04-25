using Microsoft.Extensions.Logging;

namespace Beis.LearningPlatform.Web.Controllers
{
    public class CmsControllerBase : ControllerBase
    {
        public CmsControllerBase(ILogger<CmsControllerBase> logger) : base(logger)
        {
        }
    }
}
