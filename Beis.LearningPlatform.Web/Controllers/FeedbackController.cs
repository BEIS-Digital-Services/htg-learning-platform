using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Controllers
{
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackControllerHelper _feedbackControllerHelper;

        public FeedbackController(ILogger<FeedbackController> logger, IFeedbackControllerHelper feedbackControllerHelper) : base(logger)
        {
            _feedbackControllerHelper = feedbackControllerHelper;
        }

        [Route("/feedback/report-problem", Name = "Feedback_ReportProblem")]
        public async Task<JsonResult> ProcessReport(CMSFeedbackProblem model)
        {
            if (!ModelState.IsValid)
            {
                return JsonBadRequest();
            }

            var statusCode = await _feedbackControllerHelper.ReportProblem(model);
            return Json(statusCode);
        }

        [Route("/feedback/useful", Name = "Feedback_Useful")]
        public async Task<JsonResult> ProcessFeedbackUse(CMSFeedbackPageUseful model)
        {
            if (!ModelState.IsValid)
            {
                return JsonBadRequest();
            }

            var statusCode = await _feedbackControllerHelper.IsUseful(model);
            return Json(statusCode);
        }


        [Route("/feedback/{feedback}", Name = "Feedback")]
        public async Task<IActionResult> Feedback(string feedback)
        {
            if (string.IsNullOrWhiteSpace(feedback))
            {
                return BadRequest();
            }

            await _feedbackControllerHelper.ProcessFeedback(feedback);
            var feedbackRouteUrl = _feedbackControllerHelper.GetFeedbackRouteUrl();
            return Redirect(feedbackRouteUrl);
        }
    }
}