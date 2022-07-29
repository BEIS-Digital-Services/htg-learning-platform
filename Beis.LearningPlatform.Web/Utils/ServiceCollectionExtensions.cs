using Beis.LearningPlatform.BL.DependencyInjection;
using Beis.LearningPlatform.BL.IntegrationServices.Options;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories;
using Beis.LearningPlatform.Data;
using Beis.LearningPlatform.Web.ControllerHelpers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO.Abstractions;

namespace Beis.LearningPlatform.Web.Utils
{
    internal static class ServiceCollectionExtensions
    {
        internal static void RegisterAllServices(this IServiceCollection services, IConfiguration configuration, bool useSsl)
        {
            FormSearchTagsExtensions.StrapiApiUrl = configuration["CmsConfig:ApiBaseUrl"];

            services.AddLogging(options => { options.AddConsole(); });
            if (!string.IsNullOrWhiteSpace(configuration["ApplicationInsightsConfig:Key"]))
                services.AddApplicationInsightsTelemetry(configuration["ApplicationInsightsConfig:Key"]);

            services.AddControllersWithViews().AddSessionStateTempDataProvider();

            services.AddSession(opts =>
            {
                opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                opts.Cookie.SameSite = SameSiteMode.Strict;
                opts.Cookie.HttpOnly = true;
                opts.Cookie.IsEssential = true;
            });

            // Markdown pipeline for cms content (currently instantiated in razor views, the below allows refactoring to inject into view components):
            services.AddSingleton(new MarkdownPipelineBuilder().UseAdvancedExtensions().UseBootstrap().Build());

            services.AddOptions();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            if (useSsl)
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
                    options.HttpsPort = int.Parse(configuration["HttpListenerConfig:SSLPort"]);
                });
            }

            services.AddDbContext<DataContext>(options => options.UseNpgsql(configuration["DatabaseConfig:LearningPlatformDbConnectionString"]));
            services.AddDbContext<HtgVendorSmeDbContext>(options => options.UseNpgsql(configuration["DatabaseConfig:HelpToGrowDbConnectionString"]));
            services.AddDataProtection().PersistKeysToDbContext<DataContext>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["DatabaseConfig:RedisConnectionString"];
                options.InstanceName = configuration["DatabaseConfig:RedisInstanceName"];
            });

            services.AddAutoMapper(config =>
            {
                config.AddMaps(Assembly.GetExecutingAssembly());
            });


            // Add app services
            services.AddBLServices();
            services.AddAppServices();
            services.RegisterAppOptions(configuration);

            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
        }

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
            services.AddScoped<IExpressionOfInterestControllerHelper, ExpressionOfInterestControllerHelper>();
            
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