using Beis.LearningPlatform.BL.Models;
using Beis.LearningPlatform.BL.Services;
using Beis.LearningPlatform.Web.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Services
{
    public class CmsFeedbackService : ICmsFeedbackService
    {
        private readonly IFeedbackService _feedbackService;

        public CmsFeedbackService(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<HttpStatusCode> ReportProblem(CMSFeedbackProblemBM model)
        {
            var rtn = await _feedbackService.SaveFeedBackReport(model);
            return rtn ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        public async Task<HttpStatusCode> IsUseful(CMSFeedbackPageUsefulBM model)
        {
            var rtn = await _feedbackService.SaveFeedBackPageUseful(model);
            return rtn ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        public async Task<bool> ProcessFeedback(string url, string feedback)
        {            
            var feedbackModel = new CMSFeedbackPageUsefulBM
            {
                url = url,
                IsPageUseful = feedback
            };

            return await _feedbackService.SaveFeedBackPageUseful(feedbackModel);
        }        
    }
}