using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Services;
using Beis.LearningPlatform.Web.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Controllers
{
    public class SatisfactionSurveyController : Controller
    {
        private readonly ISatisfactionSurveyControllerHelper _satisfactionSurveyControllerHelper;
        private readonly ICmsService2 _cmsService2;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public SatisfactionSurveyController(ISatisfactionSurveyControllerHelper satisfactionSurveyControllerHelper,
                                        ICmsService2 cmsService2, IHttpContextAccessor httpContextAccessor)
        {
            _satisfactionSurveyControllerHelper = satisfactionSurveyControllerHelper;
            _cmsService2 = cmsService2;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("/satisfaction-survey")]
        public async Task<IActionResult> Index()
        {
            var cmsPage = await _cmsService2.GetPage("Custom-pages/home");
            cmsPage.SetPageTitle("Help to Grow: Digital - Satisfaction Survey");
            return View(new DataPageViewModel<SatisfactionSurveyViewModel>(cmsPage, new SatisfactionSurveyViewModel
            {
                Url = _httpContextAccessor.HttpContext.GetRefererUrl()
            }, "satisfaction-survey"));
        }

        [Route("/satisfaction-survey-complete")]
        public async Task<IActionResult> Complete()
        {
            var cmsPage = await _cmsService2.GetPage("Custom-pages/home");
            cmsPage.SetPageTitle("Help to Grow: Digital - Satisfaction Survey");
            return View(new DataPageViewModel<SatisfactionSurveyViewModel>(cmsPage, new SatisfactionSurveyViewModel(), "satisfaction-survey-complete"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/satisfaction-survey")]
        public async Task<IActionResult> SubmitSurvey([FromForm] SatisfactionSurveyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!viewModel.RatingOptions.Contains(viewModel.Rating))
                {
                    return BadRequest(); // Invalid data posted
                }

                var result = await _satisfactionSurveyControllerHelper.SaveSatisfactionSurvey(viewModel);
                if (!result.Result)
                {
                    ModelState.AddModelError("SubmitError", result.Message);
                }
                else
                {
                    return RedirectToAction("Complete");
                }
            }

            var cmsPage = await _cmsService2.GetPage("Custom-pages/home");
            cmsPage.SetPageTitle("Help to Grow: Digital - Satisfaction Survey");
            return View("Index", new DataPageViewModel<SatisfactionSurveyViewModel>(cmsPage, viewModel, "satisfaction-survey-validation-error"));
        }
    }
}