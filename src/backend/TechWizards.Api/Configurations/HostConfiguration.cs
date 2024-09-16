namespace TechWizards.Api.Configurations;

/// <summary>
/// Contains configurations for the host.
/// </summary>
public static partial class HostConfiguration
{
    /// <summary>
    /// Configures application builder
    /// </summary>
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddGeneralInfra()
            .AddLogging()
            .AddCaching()
            .AddMappers()
            .AddPersistence()
            .AddValidators()
            .AddInfraComms()
            .AddTemplatesInfrastructure()
            .AddAiCapabilities()
            .AddFileStorageInfrastructure()
            .AddAssessmentInfrastructure()
            .AddCors()
            .AddDevTools()
            .AddExposers();
            
        return new(builder);
    }

    /// <summary>
    /// Configures application
    /// </summary>
    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        await app.UpdateDatabaseAsync();
        
        app
            .UseCustomCors()
            .UseLocalFileStorage()
            .UseDevTools()
            .UseExposers();

        return app;
    }
}