using Beis.LearningPlatform.Web;
using Microsoft.Extensions.Logging.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration(configuration =>
{
    configuration.AddJsonFile("diagnosticForm.json", false, true);
    var configurationRoot = configuration.Build();

    var connectionString = configurationRoot.GetConnectionString("AppConfig");
    if (connectionString != null)
    {
        configuration.AddAzureAppConfiguration(connectionString);
    }

    ApplicationForm options = new();
    configurationRoot.GetSection(nameof(ApplicationForm)).Bind(options);
}).ConfigureLogging(logging =>
{
   logging.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Debug);
   logging.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);
});

var hasParsed = bool.TryParse(builder.Configuration["HttpListenerConfig:UseSSL"], out var useSsl);

// Add services to the container.
builder.Services.AddMvcCore(r => r.EnableEndpointRouting = false);
builder.Services.RegisterAllServices(builder.Configuration, hasParsed && useSsl);

// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
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


if (hasParsed && useSsl)
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.UseMvc(r => r.MapRoute("default", "{controller=Home}/{action=Index}"));
app.Run();