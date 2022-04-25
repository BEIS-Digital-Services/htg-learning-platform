using Beis.LearningPlatform.Data.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Beis.LearningPlatform.Data
{
    public static class IdentityExtensions
    {
        /// <summary>
        /// Add Identity Services
        /// </summary>
        /// <param name="services">Service Collection</param>
        public static void AddIdentityServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            services.AddIdentityServices(configuration);
        }

        /// <summary>
        /// Add Identity Services
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="configuration">Configuration</param>
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var identityPasswordSettings = configuration.GetSection(nameof(IdentityPasswordSettings)).Get<IdentityPasswordSettings>();
            services.AddIdentityServices(identityPasswordSettings);
        }

        /// <summary>
        /// Add Identity Services
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="identityPasswordSettings">Identity Password Settings</param>
        public static void AddIdentityServices(this IServiceCollection services, IdentityPasswordSettings identityPasswordSettings)
        {
            services.AddIdentity<User, Role>
             (o =>
             {
                 o.Password.RequiredUniqueChars = identityPasswordSettings.RequiredUniqueChars;
                 o.Password.RequireDigit = identityPasswordSettings.RequireDigit;
                 o.Password.RequireLowercase = identityPasswordSettings.RequireLowercase;
                 o.Password.RequireUppercase = identityPasswordSettings.RequireUppercase;
                 o.Password.RequireNonAlphanumeric = identityPasswordSettings.RequireNonAlphanumeric;
                 o.Password.RequiredLength = identityPasswordSettings.RequiredLength;
             })
             .AddEntityFrameworkStores<DataContext>()
             .AddDefaultTokenProviders();
        }

        public static ModelBuilder ApplyIdentityConfiguration(this ModelBuilder builder)
        {
            // User Map
            builder.Entity<User>(b =>
            {
                b.Property(x => x.Id)
                .HasMaxLength(50)
                .ValueGeneratedOnAdd();

                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            // Role Map
            builder.Entity<Role>(b =>
            {
                b.Property(x => x.Id)
                .HasMaxLength(50)
                .ValueGeneratedOnAdd();

                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            return builder;
        }
    }
}