using Beis.LearningPlatform.BL.IntegrationServices.Options;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Interface;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Pricing;
using Beis.LearningPlatform.Web.Configuration;
using Beis.LearningPlatform.Web.ControllerHelpers;
using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;

namespace Beis.LearningPlatform.Web.Utils
{
    internal static class ServiceCollectionExtensions
    {
        internal static void AddAppServices(this IServiceCollection services)
        {
            // Controller helpers
            services.AddScoped<IDiagnosticToolControllerHelper, DiagnosticToolControllerHelper>();
            services.AddScoped<IEmailControllerHelper, EmailControllerHelper>();
            services.AddScoped<IHomeControllerHelper, HomeControllerHelper>();
            services.AddScoped<ISatisfactionSurveyControllerHelper, SatisfactionSurveyControllerHelper>();
            services.AddScoped<ICookieControllerHelper, CookieControllerHelper>();
            services.AddScoped<IComparisonToolControllerHelper, ComparisonToolControllerHelper>();
            services.AddScoped<IFeedbackControllerHelper, FeedbackControllerHelper>();

            // Local services
            services.AddScoped<ICmsApiIntegrationService, CmsApiIntegrationService>();
            services.AddScoped<ICmsService, CmsService>();
            services.AddScoped<ICmsService2, CmsService2>();
            services.AddScoped<ICmsFeedbackService, CmsFeedbackService>();
            services.AddScoped<IDiagnosticToolFormService, DiagnosticToolFormService>();
            services.AddScoped<IComparisonToolService, ComparisonToolService>();
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IMakeApiCallService, StrapiMakeApiCallService>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddSingleton<IHtmlTextService, HtmlTextService>();
            RegisterEmailHelpers(services);

            // VENDOR Product Repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPricingRepository, PricingRepository>();
            services.AddScoped<IProductFiltersRepository, ProductFiltersRepository>();
            services.AddScoped<IProductCapabilitiesRepository, ProductCapabilitiesRepository>();
            services.AddScoped<ISettingsProductCapabilitiesRepository, SettingsProductCapabilitiesRepository>();
            services.AddScoped<ISettingsProductFiltersRepository, SettingsProductFiltersRepository>();
            services.AddTransient<IProductCategoryDisplaySettings, ProductCategoryDisplaySettings>();
        }

        internal static void RegisterAppOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NotifyServiceOption>(configuration.GetSection(NotifyServiceOption.NotifyService));
            services.Configure<ComparisonToolDisplayOption>(configuration.GetSection(ComparisonToolDisplayOption.ComparisonToolDisplay));
            services.Configure<CookieNamesOption>(configuration.GetSection(CookieNamesOption.CookieNames));
            services.Configure<CmsOption>(configuration.GetSection(CmsOption.Cms));
            services.Configure<VoucherAppOption>(configuration.GetSection(VoucherAppOption.VoucherApp));
            services.Configure<VendorAppOption>(configuration.GetSection(VendorAppOption.VenderApp));
            services.Configure<WebsiteOption>(configuration.GetSection(WebsiteOption.Common));
            services.Configure<ApplicationForm>(configuration.GetSection(ApplicationForm.Application));
        }

        private static void RegisterEmailHelpers(IServiceCollection services)
        {
            services.AddTransient<IEmailResponseHelper, DiagnosticToolEmailResponseHelper>();
            services.AddTransient<IEmailResponseHelper, SkillsEmailResponseHelper>();
            services.AddTransient<IEmailResponseHelper, SkillsModuleTwoResponseHelper>();
            services.AddTransient<IEmailResponseHelper, SkillsModuleThreeResponseHelper>();
            services.AddTransient<IEmailResponseHelperFactory, EmailResponseHelperFactory>();
        }
    }
}