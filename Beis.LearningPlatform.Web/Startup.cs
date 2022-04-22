using Beis.Htg.VendorSme.Database;
using Beis.LearningPlatform.BL.DependencyInjection;
using Beis.LearningPlatform.Data;
using Beis.LearningPlatform.Web.Utils;
using Markdig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Reflection;

namespace Beis.LearningPlatform.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly bool _useSsl;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _ = bool.TryParse(_configuration["HttpListenerConfig:UseSSL"], out _useSsl);

            FormSearchTagsExtensions.StrapiApiUrl = configuration["CmsConfig:ApiBaseUrl"];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(options => { options.AddConsole(); });
            if (!string.IsNullOrWhiteSpace(_configuration["ApplicationInsightsConfig:Key"]))
                services.AddApplicationInsightsTelemetry(_configuration["ApplicationInsightsConfig:Key"]);

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

            if (_useSsl)
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
                    options.HttpsPort = int.Parse(_configuration["HttpListenerConfig:SSLPort"]);
                });
            }

            services.AddDbContext<DataContext>(options => options.UseNpgsql(_configuration["DatabaseConfig:LearningPlatformDbConnectionString"]));
            services.AddDbContext<HtgVendorSmeDbContext>(options => options.UseNpgsql(_configuration["DatabaseConfig:HelpToGrowDbConnectionString"]));
            services.AddDataProtection().PersistKeysToDbContext<DataContext>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = _configuration["DatabaseConfig:RedisConnectionString"];
                options.InstanceName = _configuration["DatabaseConfig:RedisInstanceName"];
            });

            services.AddAutoMapper(config =>
            {
                    config.AddMaps(Assembly.GetExecutingAssembly());
            });


            // Add app services
            services.AddBLServices();
            services.AddAppServices();
            services.RegisterAppOptions(_configuration);

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            services.AddMvcCore(r => r.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseExceptionHandler("/Home/Error");
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home/Error";
                    await next();
                }
            });


            if (_useSsl)
            {
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseMvc(r => r.MapRoute("default", "{controller=Home}/{action=Index}"));
        }
    }
}