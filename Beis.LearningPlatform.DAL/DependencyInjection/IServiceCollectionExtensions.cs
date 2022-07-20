using Beis.LearningPlatform.Data.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Beis.LearningPlatform.DAL.DependencyInjection
{
    /// <summary>
    /// A class that provides a DI bootstrap for the Infrastructure DAL.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services from the Infrastructure DAL to the service collection.
        /// </summary>
        /// <param name="serviceCollection">An IServiceCollection that is the service collection to use.</param>
        /// <returns>An IServiceCollection that is the original service collection.</returns>
        public static IServiceCollection AddDalServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDataServices();

            serviceCollection.AddAutoMapper(config =>
            {
                config.AddMaps(Assembly.GetExecutingAssembly());
            });

            // DAL services
            serviceCollection.AddTransient<IEmailDataService, EmailDataService>();
            serviceCollection.AddTransient<ISkillsOneDataService, SkillsOneDataService>();
            serviceCollection.AddTransient<ISkillsTwoDataService, SkillsTwoDataService>();
            serviceCollection.AddTransient<ISkillsThreeDataService, SkillsThreeDataService>();
            serviceCollection.AddTransient<ISatisfactionSurveyDataService, SatisfactionSurveyDataService>();
            serviceCollection.AddTransient<IFeedbackUsefulDataService, FeedbackUsefulDataService>();
            serviceCollection.AddTransient<IFeedbackProblemReportDataService, FeedbackProblemReportDataService>();
            serviceCollection.AddTransient<IExpressionOfInterestDataService, ExpressionOfInterestDataService>();
            
            return serviceCollection;
        }
    }
}