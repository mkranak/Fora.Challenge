using Fora.Challenge.Api;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

await app.CreateDatabaseIfNeeded();

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start correctly.");
}
finally
{
    Log.CloseAndFlush();
}

// partial class for integration tests
public partial class Program { }
