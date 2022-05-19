using Beis.LearningPlatform.Data.Entities.Base;
using Beis.LearningPlatform.Data.Entities.DiagnosticTool;
using Beis.LearningPlatform.Data.Entities.Feedback;
using Beis.LearningPlatform.Data.Entities.SatisfactionSurvey;
using Beis.LearningPlatform.Data.Entities.Skills;
using Beis.LearningPlatform.Data.Entities.Users;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Data
{
    public class DataContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>,
        IDataProtectionKeyContext
    {
        public static readonly LoggerFactory loggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });
        private readonly IHttpContextAccessor httpContextAccessor;

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyIdentityConfiguration();
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseLoggerFactory(loggerFactory);
#endif      
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<FeedbackProblemReport> FeedbackProblemReport { get; set; }
        public DbSet<FeedbackPageUseful> FeedbackPageUseful { get; set; }
        public DbSet<DiagnosticToolEmailAnswer> DiagnosticToolEmailAnswer { get; set; }
        public DbSet<SkillsOneResponse> SkillsOneResponse { get; set; }
        public DbSet<SkillsTwoResponse> SkillsTwoResponse { get; set; }
        public DbSet<SkillsThreeResponse> SkillsThreeResponse { get; set; }
        public DbSet<SatisfactionSurveyEntry> SatisfactionSurveyEntry { get; set; }

        private void AddTimestamps()
        {
            // get entries that are being Added or Updated    
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var userId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var currentUsername = !string.IsNullOrWhiteSpace(userId) ? userId : "Anonymous";

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as BaseEntity;

                if (entry.State == EntityState.Added)
                {
                    entity.Created = DateTime.UtcNow;
                    entity.CreatedBy = currentUsername;
                }

                entity.Updated = DateTime.UtcNow;
                entity.UpdatedBy = currentUsername;
            }
        }
    }
}