﻿namespace Beis.LearningPlatform.Data.Repositories
{
    /// <summary>
    /// Data Repository (Unit Of Work)
    /// </summary>
    public interface IDataRepository : IDisposable
    {
        IFeedbackProblemReportRepository FeedbackProblemReports { get; }

        int Save();
        Task<int> SaveAsync();
    }
}