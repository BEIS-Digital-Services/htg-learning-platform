using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System.Security.Claims;

namespace Beis.LearningPlatform.Data
{
    public class DataContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>,
        IDataProtectionKeyContext
    {
        public static readonly LoggerFactory loggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });
        private readonly IHttpContextAccessor httpContextAccessor;
        public DataContext()
        {
            
        }

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

        public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public virtual DbSet<FeedbackProblemReport> FeedbackProblemReport { get; set; }
        public virtual DbSet<FeedbackPageUseful> FeedbackPageUseful { get; set; }
        public virtual DbSet<DiagnosticToolEmailAnswer> DiagnosticToolEmailAnswer { get; set; }
        public virtual DbSet<SkillsOneResponse> SkillsOneResponse { get; set; }
        public virtual DbSet<SkillsTwoResponse> SkillsTwoResponse { get; set; }
        public virtual DbSet<SkillsThreeResponse> SkillsThreeResponse { get; set; }
        public virtual DbSet<SatisfactionSurveyEntry> SatisfactionSurveyEntry { get; set; }

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