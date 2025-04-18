﻿using LearnMicroservice.IDP.Extensions;
using LearnMicroservice.IDP.Persistence;
using Serilog;

Log.Information("Starting up");
var builder = WebApplication.CreateBuilder(args);
try
{
    builder.AddAppConfigurations();
    builder.Host.ConfigureSerilog();

    var app = builder
            .ConfigureServices()
            .ConfigurePipeline()
        ;
    //await app.MigrateDatabaseAsync(builder.Configuration);
    // Migration must be done before seeding data
    await SeedUserData.EnsureSeedDataAsync(builder.Configuration.GetConnectionString("IdentitySqlConnection"));

    // --------- Create config to show all endpoint that have been registered.------
    //app.UseEndpoints(endpoints =>
    //{
    //    endpoints.MapControllers();
    //    foreach (var endpoint in endpoints.DataSources.SelectMany(ds => ds.Endpoints))
    //    {
    //        Console.WriteLine(endpoint.DisplayName);
    //    }
    //});

    app.MigrateDatabaseAsync(builder.Configuration).GetAwaiter().GetResult().Run();
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