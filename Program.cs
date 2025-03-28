using LearnMicroservice.IDP.Extensions;
using Serilog;

Log.Information("Starting up");
var builder = WebApplication.CreateBuilder(args);
try
{
    builder.AddAppConfigurations();
    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
            .ConfigureServices()
            .ConfigurePipeline()
        ;
    //await app.MigrateDatabaseAsync(builder.Configuration);
    // Migration must be done before seeding data
    //await builder.Services.EnsureSeedDataAsync();

    app.Run();
}

catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information($"Shutdown {builder.Environment.ApplicationName} complete");
    Log.CloseAndFlush();
}