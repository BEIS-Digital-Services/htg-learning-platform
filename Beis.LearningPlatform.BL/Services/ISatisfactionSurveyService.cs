using Beis.LearningPlatform.BL.Domain;
using Beis.LearningPlatform.Library;
using System;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.BL.Services
{
    public interface ISatisfactionSurveyService
	{
		Task<IServiceResponse<int>> SaveSatisfactionSurvey(Guid requestID, SatisfactionSurveyDto satisfactionSurveyDto);
	}
}