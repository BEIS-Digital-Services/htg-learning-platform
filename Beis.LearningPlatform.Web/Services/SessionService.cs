using System.Text;
using Newtonsoft.Json;

namespace Beis.LearningPlatform.Web.Services
{
    public class SessionService : ISessionService
    {
        public void Set(string key, object value, HttpContext currentContext)
        {
            var serialized = JsonConvert.SerializeObject(value);
            var bytes = Encoding.UTF8.GetBytes(serialized);
            currentContext.Session.Set(key, bytes);
        }

        public bool TryGet<T>(string key, HttpContext currentContext, out T value)
        {
            if (!currentContext.Session.TryGetValue(key, out var bytes))
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

        public bool HasValidSession(HttpContext currentContext)
        {
            var isAvailable = currentContext?.Session.IsAvailable ?? false;

            return isAvailable && currentContext.Session.Keys.Any();
        }
    
        public void Remove(string key, HttpContext currentContext)
        {
            currentContext.Session.Remove(key);
        }
    }
}