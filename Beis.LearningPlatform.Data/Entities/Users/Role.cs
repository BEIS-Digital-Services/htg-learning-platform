using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Data.Entities.Users
{
    public class Role : IdentityRole<string>
    {
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}