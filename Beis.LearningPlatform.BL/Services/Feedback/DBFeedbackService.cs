using AutoMapper;
using Beis.LearningPlatform.BL.Models;
using Beis.LearningPlatform.DAL;
using Beis.LearningPlatform.Library.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.BL.Services
{
    public class DBFeedbackService : IFeedbackService
    {
        public DBFeedbackService(ILogger<DBFeedbackService> logger,
                                    IMapper mapper,
                                    IFeedbackProblemReportDataService feedbackReportProblemDataService,
                                    IFeedbackUsefulDataService feedbackUsefulDataService)
        {
            _logger = logger;
            _mapper = mapper;
            _feedbackReportProblemDataService = feedbackReportProblemDataService;
            _feedbackUsefulDataService = feedbackUsefulDataService;
        }

        private readonly IFeedbackProblemReportDataService _feedbackReportProblemDataService;
        private readonly IFeedbackUsefulDataService _feedbackUsefulDataService;
        private readonly ILogger<DBFeedbackService> _logger;
        private readonly IMapper _mapper;


        public async Task<bool> SaveFeedBackPageUseful(CMSFeedbackPageUsefulBM feedback)
        {
            var feedbackInput = feedback.IsPageUseful?.ToLower().Trim();
            if (!new string[] { "yes", "no" }.Contains(feedbackInput))
            {
                _logger.LogWarning($"{nameof(SaveFeedBackPageUseful)}: invalid input {feedback.IsPageUseful}");
                return false;
            }

            if (string.IsNullOrWhiteSpace(feedback.url))
            {
                throw new ApplicationException($"{nameof(SaveFeedBackPageUseful)} missing url");
            }

            feedback.IsPageUseful = feedback.IsPageUseful.Replace("yes", "true", StringComparison.OrdinalIgnoreCase).Replace("no", "false", StringComparison.OrdinalIgnoreCase);
            var feedbackDto = _mapper.Map<FeedbackPageUsefulDto>(feedback);
            feedbackDto.Date = DateTime.Now;

            var rtn = await _feedbackUsefulDataService.Add(feedbackDto);
            return rtn != default;
        }


        public async Task<bool> SaveFeedBackReport(CMSFeedbackProblemBM problemReport)
        {
            if (string.IsNullOrWhiteSpace(problemReport.url))
            {
                throw new ApplicationException($"{nameof(SaveFeedBackReport)} missing url");
            }

            var feedbackDto = _mapper.Map<FeedbackProblemReportDto>(problemReport);
            feedbackDto.Date = DateTime.Now;
            var rtn = await _feedbackReportProblemDataService.Add(feedbackDto);
            return rtn != default;
        }
    }
}