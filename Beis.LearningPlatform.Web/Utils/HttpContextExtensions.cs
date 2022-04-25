using Microsoft.AspNetCore.Http;
using System;

namespace Beis.LearningPlatform.Web.Utils
{
    public static class HttpContextExtensions
	{
		public static string GetRefererUrl(this HttpContext httpContext, bool returnRelativeUrl = false)
		{
			if (!httpContext.Request.Headers.ContainsKey("Referer"))
			{
				return null;
			}
			var refererHeaderValue = httpContext.Request.Headers["Referer"].ToString();
			if (string.IsNullOrWhiteSpace(refererHeaderValue))
			{
				return refererHeaderValue;
			}

            if (returnRelativeUrl && Uri.TryCreate(refererHeaderValue, UriKind.Absolute, out Uri absoluteUrl)) 
            { 
                return absoluteUrl.PathAndQuery;
            }

            return refererHeaderValue;
		}

        public static string GetSessionId(this HttpContext httpContext)
        {
            return httpContext?.Session?.Id;
        }
	}
}