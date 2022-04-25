using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using System;

namespace Beis.LearningPlatform.Web
{
    public class Program
    {
        private static ILogger _logger;

        public static void Main(string[] args)
        {
            try
            {
                CreateLogger();

                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A critical error occurred");
                throw;
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseStartup<Startup>();
                   })
                   .ConfigureAppConfiguration(configuration =>
                   {
                       configuration.AddJsonFile("diagnosticForm.json", false, true);
                       var configurationRoot = configuration.Build();
                       ApplicationForm options = new();
                       configurationRoot.GetSection(nameof(ApplicationForm)).Bind(options);
                   })
                   .ConfigureLogging(logging =>
                   {
                       logging.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Debug);
                       logging.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);
                   });
        }

        private static void CreateLogger()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
                builder.SetMinimumLevel(LogLevel.Debug);
            });
            _logger = loggerFactory.CreateLogger<Program>();
        }
    }
}