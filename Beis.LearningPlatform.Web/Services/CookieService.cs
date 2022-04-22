using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Services
{
    public class CookieService : ICookieService
    {
        private const string SessionKeyIsCookieBannerClosed = "IsCookieBannerClosed";

        private static readonly IList<string> CookieValues = new List<string> { "on", "t", "true", "y", "yes" };

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cookieNameHtgJsEnabledCookie;
        private readonly string _cookieNameHtgRememberSettingsCookie;
        private readonly string _cookieNameIsHtgAccepted;
        private readonly string _cookieNameIsGaAccepted;
        private readonly string _cookieNameHtgMarketingCookie;
        private readonly Dictionary<string, string[]> _cookieTypeNameMapping;

        public CookieService(IOptions<CookieNamesOption> cookieNamesOptions, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var cookieNamesOption = cookieNamesOptions.Value;
            _cookieNameIsGaAccepted = cookieNamesOption.NonEssential?.HtGAnalyticsCookie;
            _cookieNameIsHtgAccepted = cookieNamesOption.Essential?.HtGcookieAcceptedCookie;
            _cookieNameHtgJsEnabledCookie = cookieNamesOption.Essential?.HtGJSEnabledCookie;
            _cookieNameHtgRememberSettingsCookie = cookieNamesOption.Essential?.HtGRememberSettingsCookie;
            _cookieNameHtgMarketingCookie = cookieNamesOption.NonEssential?.HtGMarketingCookie;

            _cookieTypeNameMapping = new Dictionary<string, string[]> {
                { "HTG", new string[] { _cookieNameIsHtgAccepted } },
                { "GA", new string[] { _cookieNameIsGaAccepted, _cookieNameHtgMarketingCookie } }
            };
        }

        public UserCookiePreferencesModel GetUserCookiePreferences()
        {
            return new UserCookiePreferencesModel
            {
                IsCookieBannerClosed = IsCookieBannerClosed(),
                IsGaAccepted = GetBooleanCookieValue(_cookieNameIsGaAccepted),
                IsHtgAccepted = GetBooleanCookieValue(_cookieNameIsHtgAccepted),
                IsJavascriptEnabled = GetBooleanCookieValue(_cookieNameHtgJsEnabledCookie),
                RememberSettings = GetBooleanCookieValue(_cookieNameHtgRememberSettingsCookie),
                MarketingAccepted = GetBooleanCookieValue(_cookieNameHtgMarketingCookie)
            };
        }

        public void CloseCookieBanner()
        {
            _httpContextAccessor.HttpContext?.Session.SetString(SessionKeyIsCookieBannerClosed, "true");
        }

        private bool IsCookieBannerClosed()
        {
            if (this.GetBooleanCookieValue(_cookieNameIsGaAccepted) ?? false)
            {
                return true;
            }

            var sessionValue = _httpContextAccessor.HttpContext?.Session.GetString(SessionKeyIsCookieBannerClosed);
            if (!bool.TryParse(sessionValue, out bool isCookieBannerClosed))
            {
                return false;
            }
            return isCookieBannerClosed;
        }


        private bool? GetBooleanCookieValue(string cookieName)
        {
            if (TryGetCookieValue(_httpContextAccessor.HttpContext?.Request.Cookies, cookieName, out bool cookieValue))
            {
                return cookieValue;
            }
            return default;
        }

        public void SetIsGaAccepted(bool accepted)
        {
            SetBooleanCookie(_cookieNameIsGaAccepted, accepted);
        }


        public void SetIsHtgAccepted(bool accepted)
        {
            SetBooleanCookie(_cookieNameIsHtgAccepted, accepted);
        }

        public void SetIsJavascriptEnabled(bool enabled)
        {
            SetBooleanCookie(_cookieNameHtgJsEnabledCookie, enabled);
        }

        public void SetRememberSettings(bool remember)
        {
            SetBooleanCookie(_cookieNameHtgRememberSettingsCookie, remember);
        }

        public void SetMarketingAccepted(bool accepted)
        {
            SetBooleanCookie(_cookieNameHtgMarketingCookie, accepted);
        }

        public void SaveCookiesPreferences(SaveCookiePreferenceModel saveCookiePreferenceModel)
        {
            SetBooleanCookie(_cookieNameIsGaAccepted, saveCookiePreferenceModel.GoogleAnalyticsCookies ?? false);
            SetBooleanCookie(_cookieNameHtgMarketingCookie, saveCookiePreferenceModel.MarketingCookies ?? false);
            SetBooleanCookie(_cookieNameHtgRememberSettingsCookie, saveCookiePreferenceModel.RememberSettingsCookies ?? false);
        }

        public void ProcessCookie(string cookieType, bool accepted)
        {
            if (cookieType == "close")
            {
                CloseCookieBanner();
                return;
            }

            if (!_cookieTypeNameMapping.ContainsKey(cookieType))
            {
                throw new ArgumentException($"No mapping for {nameof(cookieType)}: {cookieType}");
            }

            var cookieNames = _cookieTypeNameMapping[cookieType];
            foreach (var cookieName in cookieNames)
            {
                SetBooleanCookie(cookieName, accepted);
            }
        }

        private void SetBooleanCookie(string cookieName, bool accepted)
        {
            this._httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, $"{accepted}".ToLower(), GetDefaultCookieOptions());
        }

        private CookieOptions GetDefaultCookieOptions()
        {
            return new CookieOptions
            {
                Expires = DateTime.Now.AddYears(2),
                SameSite = SameSiteMode.Strict,
                Secure = true
            };
        }

        private static bool TryGetCookieValue(IRequestCookieCollection cookieCollection, string name, out bool value)
        {
            bool returnValue = false;

            // Defaults
            value = false;

            if (TryGetCookieValue(cookieCollection, name, out string cookieValue))
            {
                value = CookieValues.Contains(cookieValue.ToLower());
                returnValue = true;
            }

            return returnValue;
        }

        private static bool TryGetCookieValue(IRequestCookieCollection cookieCollection, string name, out string value)
        {
            return cookieCollection.TryGetValue(name, out value);
        }
    }
}