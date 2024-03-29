﻿namespace Beis.LearningPlatform.Web.Interfaces
{
    public interface ICmsFeedbackService
    {
        public Task<HttpStatusCode> ReportProblem(CMSFeedbackProblemBM model);
 
        public Task<HttpStatusCode> IsUseful(CMSFeedbackPageUsefulBM model);
        
        public Task<bool> ProcessFeedback(string url, string feedback);
    }
}