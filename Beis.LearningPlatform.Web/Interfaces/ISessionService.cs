namespace Beis.LearningPlatform.Web.Interfaces;

public interface ISessionService
{
    public void Set(string key, object value, HttpContext currentContext);

    public bool TryGet<T>(string key, HttpContext currentContext, out T value);

    public bool HasValidSession(HttpContext httpContext);

    public void Remove(string key, HttpContext httpContext);
}