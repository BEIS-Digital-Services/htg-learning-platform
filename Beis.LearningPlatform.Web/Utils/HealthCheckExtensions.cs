﻿using Beis.HelpToGrow.Common.Helpers;
using Beis.HelpToGrow.Common.Services.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Beis.LearningPlatform.Web.Utils;

public static class HealthCheckExtensions
{

	internal static void RegisterHealthcheckServices(this IServiceCollection services)
	{
		services.AddHealthChecks()
		  .AddCheck<DependencyInjectionHealthCheckService>(
			"Dependency Injection Health Checks",
			HealthStatus.Unhealthy,
			tags: new[]
			{
					HealthCheckType.LowFrequency.ToString(),
					HealthCheckType.DI.ToString()
			})
		  .AddCheck<DatabaseHealthCheckService>(
			"Database",
			HealthStatus.Unhealthy,
			tags: new[]
			{
					HealthCheckType.HighFrequency.ToString(),
					HealthCheckType.IO.ToString(),
					HealthCheckType.Database.ToString()
			})
		   .AddCheck<StrapiHealthCheckService>(
			"Strapi Health",
			HealthStatus.Unhealthy,
			tags: new[]
			{
					HealthCheckType.HighFrequency.ToString(),
					HealthCheckType.IO.ToString()
			});
	}

	internal static void MapSMEHealthChecks(this IEndpointRouteBuilder endpoints)
	{
		// base healthcheck
		endpoints.MapHealthChecks("/healthz", new HealthCheckOptions()
		{
			Predicate = (check) => false,
			ResponseWriter = HealthCheckJsonResponseWriter.Write
		});

		endpoints.MapHealthChecks("/healthz/low-frequency", new HealthCheckOptions()
		{
			Predicate = (check) => check.Tags.Contains(HealthCheckType.LowFrequency.ToString()),
			ResponseWriter = HealthCheckJsonResponseWriter.Write
		});

		endpoints.MapHealthChecks("/healthz/high-frequency", new HealthCheckOptions()
		{
			Predicate = (check) => check.Tags.Contains(HealthCheckType.HighFrequency.ToString()),
			ResponseWriter = HealthCheckJsonResponseWriter.Write
		});

		endpoints.MapHealthChecks("/healthz/io", new HealthCheckOptions()
		{
			Predicate = (check) => check.Tags.Contains(HealthCheckType.IO.ToString()),
			ResponseWriter = HealthCheckJsonResponseWriter.Write
		});

		endpoints.MapHealthChecks("/healthz/database", new HealthCheckOptions()
		{
			Predicate = (check) => check.Tags.Contains(HealthCheckType.Database.ToString()),
			ResponseWriter = HealthCheckJsonResponseWriter.Write
		});

		endpoints.MapHealthChecks("/healthz/di", new HealthCheckOptions()
		{
			Predicate = (check) => check.Tags.Contains(HealthCheckType.DI.ToString()),
			ResponseWriter = HealthCheckJsonResponseWriter.Write
		});
	}

}
