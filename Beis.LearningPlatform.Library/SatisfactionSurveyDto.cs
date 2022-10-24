using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Library
{
    [ExcludeFromCodeCoverage(Justification = "Basic DTO - only data fields, no functions")]
    public class SatisfactionSurveyDto : DtoBase
    {
        public string Rating { get; set; }
        public string Comment { get; set; }
    }
}