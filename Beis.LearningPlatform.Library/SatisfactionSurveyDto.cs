using Beis.LearningPlatform.Library.DTO.Base;

namespace Beis.LearningPlatform.Library
{
    public class SatisfactionSurveyDto : DtoBase
    {
        public string Rating { get; set; }
        public string Comment { get; set; }
    }
}