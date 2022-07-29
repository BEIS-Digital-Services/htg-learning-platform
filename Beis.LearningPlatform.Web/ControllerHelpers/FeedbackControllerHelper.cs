namespace Beis.LearningPlatform.Web.ControllerHelpers
{

    public class FeedbackControllerHelper : CmsControllerHelperBase, IFeedbackControllerHelper
    {
        private readonly IMapper _mapper;

        public FeedbackControllerHelper(ILogger<FeedbackControllerHelper> logger,
                              ICmsService cmsService,
                              ICmsFeedbackService cmsFeedbackService,
                              IOptions<CmsOption> cmsOptions,
                              IMapper mapper,
                              IHttpContextAccessor httpContextAccessor
            ) : base(logger, cmsOptions, cmsService, cmsFeedbackService, httpContextAccessor)
        {
            _mapper = mapper;
        }


        public async Task<HttpStatusCode> ReportProblem(CMSFeedbackProblem model)
        {
            var modelBm = _mapper.Map<CMSFeedbackProblemBM>(model);
            modelBm.sessionId = _httpContextAccessor.HttpContext.GetSessionId();
            return await _cmsFeedbackService.ReportProblem(modelBm);
        }

        public async Task<HttpStatusCode> IsUseful(CMSFeedbackPageUseful model)
        {
            var modelBm = _mapper.Map<CMSFeedbackPageUsefulBM>(model);
            modelBm.sessionId = _httpContextAccessor.HttpContext.GetSessionId();
            return await _cmsFeedbackService.IsUseful(modelBm);
        }

        public string GetFeedbackRouteUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var strBaseUrl = request?.Scheme + "://" + request?.Host;
            var routeUrl = request?.Headers["Referer"].ToString().Replace(strBaseUrl, string.Empty) + "?feedback-submitted=true";
            return (routeUrl?.IndexOf("/#feedback-prompt") > -1 ? routeUrl : routeUrl + "/#feedback-prompt").Replace("//#", "/#");
        }

        public async Task<bool> ProcessFeedback(string feedback)
        {
            var url = _httpContextAccessor.HttpContext.GetRefererUrl();
            return await _cmsFeedbackService.ProcessFeedback(url, feedback);
        }
    }
}