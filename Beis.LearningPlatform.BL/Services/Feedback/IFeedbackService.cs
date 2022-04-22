using Beis.LearningPlatform.BL.Models;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.BL.Services
{
    public interface IFeedbackService
    {
        public Task<bool> SaveFeedBackPageUseful(CMSFeedbackPageUsefulBM feedback);
        public Task<bool> SaveFeedBackReport(CMSFeedbackProblemBM problemReport);
    }
}