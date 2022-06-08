namespace Beis.LearningPlatform.Data.Models.Base
{
    public class BaseModel : Model
    {
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}