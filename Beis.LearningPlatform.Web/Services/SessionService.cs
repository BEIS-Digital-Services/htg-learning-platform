using System.Text;
using Newtonsoft.Json;

namespace Beis.LearningPlatform.Web.Services
{
    public class SessionService : ISessionService
    {
        public void Set(string key, object value, HttpContext httpContext)
        {
            var serialized = JsonConvert.SerializeObject(value);
            var bytes = Encoding.UTF8.GetBytes(serialized);
            httpContext.Session.Set(key, bytes);
        }

        public bool TryGet<T>(string key, HttpContext httpContext, out T value)
        {
            if (!httpContext.Session.TryGetValue(key, out var bytes))
            {
                value = default;
                return false;
            }

            var sessionValue = Encoding.UTF8.GetString(bytes);

            if (!string.IsNullOrWhiteSpace(sessionValue))
            {
                try
                {
                    value = JsonConvert.DeserializeObject<T>(sessionValue);
                    return true;
                }
                catch
                {
                    value = default;
                    return false;
                }

            }

            value = default;
            return false;
        }

        public bool HasValidSession(HttpContext httpContext)
        {
            var isAvailable = httpContext?.Session.IsAvailable ?? false;

            return isAvailable && httpContext.Session.Keys.Any();
        }
    
        public void Remove(string key, HttpContext httpContext)
        {
            httpContext.Session.Remove(key);
        }
    }
}