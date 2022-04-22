using System;

namespace Beis.LearningPlatform.Data.Entities.Base
{
    public class BaseEntity : Entity
    {
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}