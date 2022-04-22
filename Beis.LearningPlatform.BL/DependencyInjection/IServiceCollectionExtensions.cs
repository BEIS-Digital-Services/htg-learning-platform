using Beis.LearningPlatform.BL.IntegrationServices;
using Beis.LearningPlatform.BL.IntegrationServices.GovUkNotify;
using Beis.LearningPlatform.BL.Services;
using Beis.LearningPlatform.DAL.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Beis.LearningPlatform.BL.DependencyInjection
{
    /// <summary>
    /// A class that provides a DI bootstrap for the Infrastructure BL.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services from the Infrastructure BL to the service collection.
        /// </summary>
        /// <param name="serviceCollection">An IServiceCollection that is the service collection to use.</param>
        /// <returns>An IServiceCollection that is the original service collection.</returns>
        public static IServiceCollection AddBLServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDalServices();

            serviceCollection.AddAutoMapper(config =>
            {
                config.AddMaps(Assembly.GetExecutingAssembly());
            });

            // BL services
            serviceCollection.AddTransient<IEmailService, EmailService>();
            serviceCollection.AddTransient<ISkillsOneService, SkillsOneService>();
            serviceCollection.AddTransient<ISkillsTwoService, SkillsTwoService>();
            serviceCollection.AddTransient<ISatisfactionSurveyService, SatisfactionSurveyService>();
            serviceCollection.AddTransient<IFeedbackService, DBFeedbackService>();

            // Integration services
            serviceCollection.AddTransient<INotifyIntegrationService, NotifyIntegrationService>();

            // External services
            serviceCollection.AddTransient<INotifyService, NotifyService>();

            return serviceCollection;
        }
    }
}