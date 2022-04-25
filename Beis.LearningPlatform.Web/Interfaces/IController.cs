using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Beis.LearningPlatform.Web.Interfaces
{
    public interface IController
    {
        ControllerContext ControllerContext { get; }
        ViewDataDictionary ViewData { get; }
        ITempDataDictionary TempData { get; }
    }
}