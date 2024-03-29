﻿namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface IFeedbackControllerHelper : IControllerHelperBase
    {
        Task<HttpStatusCode> ReportProblem(CMSFeedbackProblem model);
        Task<HttpStatusCode> IsUseful(CMSFeedbackPageUseful model);
        string GetFeedbackRouteUrl();
        Task<bool> ProcessFeedback(string feedback);
    }
}