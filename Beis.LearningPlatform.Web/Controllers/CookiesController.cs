namespace Beis.LearningPlatform.Web.Controllers
{
    public class CookiesController : Controller, IController
    {
        private readonly ICookieControllerHelper _cookieControllerHelper;

        public CookiesController(ICookieControllerHelper cookieControllerHelper)
        {
            _cookieControllerHelper = cookieControllerHelper;
        }


        [Route("/cookies")]
        public async Task<IActionResult> Cookies(bool cookieProcessed = false)
        {
            var result = _cookieControllerHelper.GetUserCookiePreferences();
            if (!result.Result)
            {
                return BadRequest();
            }

            var cmsPageViewModel = await _cookieControllerHelper.ProcessGetCustomPageResult("Custom-pages/cookies");
            var viewModel = new DataPageViewModel<CookiePageViewModel>(cmsPageViewModel, new CookiePageViewModel
            {
                UserCookiePreferences = result.Payload,
                CookieProcessed = cookieProcessed
            }, "cookie-page");
            
            return View("Cookies", viewModel);
        }

        [Route("/ProcessCookie/{controllerName?}/{actionName?}/{cookieType?}/{isAccept?}")]
        public IActionResult ProcessCookie(string controllerName, string actionName, string cookieType, bool isAccept)
        {
            var response = _cookieControllerHelper.ProcessCookie(cookieType, isAccept);
            if (!response.Result)
            {
                return BadRequest();
            }

            if (_cookieControllerHelper.SafeRedirectToReferer(out string redirectUrl))
            {
                return Redirect(redirectUrl);
            }
            return RedirectToAction(actionName, controllerName);
        }

        [HttpPost]
        [Route("/cookies/save-preferences")]
        public IActionResult SaveCookiesPreferences(SaveCookiePreferenceModel saveCookiePreferenceModel)
        {
            var response = _cookieControllerHelper.SaveCookiesPreferences(saveCookiePreferenceModel);
            if (!response.Result)
            {
                return BadRequest();
            }

            return RedirectToAction("Cookies", new { cookieProcessed = true });
        }
    }
}