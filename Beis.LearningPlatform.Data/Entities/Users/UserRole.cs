using Microsoft.AspNetCore.Identity;

namespace Beis.LearningPlatform.Data.Entities.Users
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}