using Beis.LearningPlatform.Data.Entities.Base;

namespace Beis.LearningPlatform.Data.Entities.SatisfactionSurvey
{
    public class SatisfactionSurveyEntry : FeedbackEntity
	{
		public string rating { get; set; }
		public string comment { get; set; }
	}
}