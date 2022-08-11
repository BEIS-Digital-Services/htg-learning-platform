using Microsoft.AspNetCore.Mvc.Filters;

namespace Beis.LearningPlatform.Web.Filters
{
    public class ExceptionInterceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new RedirectToRouteResult("ErrorGet", new RouteValueDictionary());
            return Task.CompletedTask;
        }
    }
}