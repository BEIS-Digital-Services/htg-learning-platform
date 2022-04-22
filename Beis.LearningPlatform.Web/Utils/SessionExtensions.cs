using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace Beis.LearningPlatform.Web.Utils
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Clears the specified session data.
        /// </summary>
        /// <param name="session">An ISession that is the session to get the data from.</param>
        /// <param name="name">A string that is the name of the data to get.</param>
        public static void ClearSessionData(this ISession session, string name)
        {
            session.SetSessionData<object>(name, null);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            T returnValue = default;

            var json = session.GetString(key);
            if (json != null)
                returnValue = JsonSerializer.Deserialize<T>(json);

            return returnValue;
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            string json = JsonSerializer.Serialize(value);
            session.SetString(key, json);
        }

        /// <summary>
        /// Sets the data of the specified session data.
        /// </summary>
        /// <typeparam name="T">The Type of the data to set.</typeparam>
        /// <param name="session">An ISession that is the session to set the data from.</param>
        /// <param name="name">A string that is the name of the data to set.</param>
        /// <param name="data">A T that is the value of the data, or default.</param>
        public static void SetSessionData<T>(this ISession session, string name, T data)
            where T : class
        {
            session.SetObject(name, data);
        }

        /// <summary>
        /// Tries to get the specified data from the session.
        /// </summary>
        /// <typeparam name="T">The Type of the data to get.</typeparam>
        /// <param name="session">An ISession that is the session to get the data from.</param>
        /// <param name="name">A string that is the name of the data to get.</param>
        /// <param name="data">A T that will be set to the value of the data, or default.</param>
        /// <returns>A bool indicating whether the data was obtained from the session.</returns>
        public static bool TryGetSessionData<T>(this ISession session, string name, out T data)
            where T : class
        {
            data = session.GetObject<T>(name);
            return data != default;
        }
    }
}
