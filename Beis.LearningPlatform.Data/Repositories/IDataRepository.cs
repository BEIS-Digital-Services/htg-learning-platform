using Beis.LearningPlatform.Data.Repositories.Feedback;
using Beis.LearningPlatform.Data.Repositories.Locations;
using System;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Data.Repositories
{
    /// <summary>
    /// Data Repository (Unit Of Work)
    /// </summary>
    public interface IDataRepository : IDisposable
    {
        ILocationRepository Locations { get; }

        IFeedbackProblemReportRepository FeedbackProblemReports { get; }

        int Save();
        Task<int> SaveAsync();
    }
}