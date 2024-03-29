﻿namespace Beis.LearningPlatform.Data.DependencyInjection
{
    /// <summary>
    /// A class that provides a DI bootstrap for the Infrastructure Data layer.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services from the Infrastructure Data layer to the service collection.
        /// </summary>
        /// <param name="serviceCollection">An IServiceCollection that is the service collection to use.</param>
        /// <returns>An IServiceCollection that is the original service collection.</returns>
        public static IServiceCollection AddDataServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFeedbackProblemReportRepository, FeedbackProblemReportRepository>();
            serviceCollection.AddTransient<IFeedbackPageUsefulRepository, FeedbackPageUsefulRepository>();
            serviceCollection.AddTransient<IDiagnosticToolEmailAnswerRepository, DiagnosticToolEmailAnswerRepository>();
            serviceCollection.AddTransient<ISkillsOneResponseRepository, SkillsOneResponseRepository>();
            serviceCollection.AddTransient<ISkillsTwoResponseRepository, SkillsTwoResponseRepository>();
            serviceCollection.AddTransient<ISkillsThreeResponseRepository, SkillsThreeResponseRepository>();
            serviceCollection.AddTransient<ISatisfactionSurveyRepository, SatisfactionSurveyRepository>();
            serviceCollection.AddTransient<IExpressionOfInterestRepository, ExpressionOfInterestRepository>();            

            serviceCollection.AddTransient<IDataRepository, DataRepository>();

            return serviceCollection;
        }
    }
}