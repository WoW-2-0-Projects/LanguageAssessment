using System.Reflection;
using Backbone.AiCapabilities.NaturalLanguageProcessing.ChatCompletion.OpenAi.Configurations;
using Backbone.AiCapabilities.SemanticKernel.Abstractions.OpenAi.Configurations;
using Backbone.AiCapabilities.SemanticKernel.Abstractions.OpenAi.Settings;
using Backbone.Comms.Infra.EventBus.InMemory.Configurations;
using Backbone.Comms.Infra.Mediator.MassTransit.Configurations;
using Backbone.Comms.Infra.Mediator.MediatR.Behaviors.Configurations;
using Backbone.Comms.Infra.Mediator.MediatR.Configurations;
using Backbone.DataAccess.Relational.EfCore.Abstractions.Constants;
using Backbone.DataAccess.Relational.EfCore.Abstractions.Extensions;
using Backbone.General.Serialization.Json.Newtonsoft.Configurations;
using Backbone.General.Time.Provider.Configurations;
using Backbone.General.Validations.Abstractions.Configurations;
using Backbone.General.Validations.FluentValidation.Configurations;
using Backbone.Storage.Cache.InMemory.Lazy.Configurations;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Refit;
using Serilog;
using TechWizards.Application.GrammarAssessments.Services;
using TechWizards.Application.ListeningAssessments.Services;
using TechWizards.Application.QuizAssessmentSessions.Services;
using TechWizards.Application.QuizOptions.Services;
using TechWizards.Application.QuizQuestions.Services;
using TechWizards.Infrastructure.GrammarAssessments.Services;
using TechWizards.Infrastructure.GrammarAssessments.Settings;
using TechWizards.Infrastructure.ListeningAssessments.Services;
using TechWizards.Infrastructure.ListeningAssessments.Settings;
using TechWizards.Infrastructure.QuizAssessmentSessions.Services;
using TechWizards.Infrastructure.QuizOptions.Services;
using TechWizards.Infrastructure.QuizQuestions.Services;
using TechWizards.Persistence.DataContexts;
using TechWizards.Persistence.Repositories;
using TechWizards.Persistence.Repositories.Interfaces;
using Temporary;
using Temporary.TextTemplates.Services;

namespace TechWizards.Api.Configurations;

/// <summary>
/// Contains configurations for the host.
/// </summary>
public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies = Assembly
        .GetExecutingAssembly()
        .GetReferencedAssemblies()
        .Select(Assembly.Load)
        .Append(Assembly.GetExecutingAssembly())
        .ToList();

    ///<summary>
    /// Adds general infrastructure.
    /// </summary>
    private static WebApplicationBuilder AddGeneralInfra(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddTimeProvider()
            .AddNewtonsoftJsonSerializer();
        
        builder.Services.Configure<DomainSettings>(builder.Configuration.GetSection(nameof(DomainSettings)));

        return builder;
    }

    /// <summary>
    /// Registers logging services
    /// </summary>
    private static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
        builder.Host.UseSerilog(logger);

        return builder;
    }

    /// <summary>
    /// Adds caching services.
    /// </summary>
    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        builder.Services.AddInMemoryCacheStorageWithLazyInMemoryCacheStorage(builder.Configuration);

        return builder;
    }

    /// <summary>
    /// Registers mapping services
    /// </summary>
    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);
        return builder;
    }

    /// <summary>
    /// Registers persistence infrastructure
    /// </summary>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        // Register db context
        builder.Services.AddDbContext<AppDbContext>((_, options) =>
        {
            options.EnableSensitiveDataLogging();
            options.UseNpgsql(builder.Configuration
                .GetConnectionString(DataAccessConstants.DefaultDatabaseConnectionString));
        });

        return builder;
    }

    /// <summary>
    /// Configures the Dependency Injection container to include validators from referenced assemblies.
    /// </summary>
    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddGeneralValidationSettings(builder.Configuration);
        builder.Services.AddFluentValidation(Assemblies);

        return builder;
    }

    /// <summary>
    /// Adds communication services.
    /// </summary>
    private static WebApplicationBuilder AddInfraComms(this WebApplicationBuilder builder)
    {
        // Add a mediator pipeline with MediatR
        builder.Services
            .AddMediatRServices(Assemblies, (mediatorConfiguration, _) => mediatorConfiguration.AddMediatRPipelineBehaviors())
            .AddMediatorWithMediatR();

        // Add a mediator pipeline with MassTransit and in-memory event bus
        builder.Services
            .AddMassTransitServices(Assemblies, (config, _) => config
                .AddInMemoryEventBusWithMassTransit(builder.Services, true));

        return builder;
    }
    
    /// <summary>
    /// Registers file storage infrastructure
    /// </summary>
    private static WebApplicationBuilder AddTemplatesInfrastructure(this WebApplicationBuilder builder)
    {
        // Register settings
        builder.Services
            .Configure<TemplateRenderingSettings>(builder.Configuration.GetSection(nameof(TemplateRenderingSettings)));

        // Register foundation services
        builder.Services
            .AddSingleton<ITextTemplateRenderingService, BasicTextTemplateRenderingService>();

        return builder;
    }
    
    /// <summary>
    /// Registers file storage infrastructure
    /// </summary>
    private static WebApplicationBuilder AddFileStorageInfrastructure(this WebApplicationBuilder builder)
    {
        // Register settings
        builder.Services
            .Configure<LocalFileStorageSettings>(options => options.RootPath = builder.Environment.WebRootPath);

        return builder;
    }

    /// <summary>
    /// Registers semantic kernel infrastructure.
    /// </summary>
    private static WebApplicationBuilder AddAiCapabilities(this WebApplicationBuilder builder)
    {
        // Register Semantic Kernel with OpenAI
        builder.Services.AddSemanticKernelWithOpenAi(builder.Configuration, (kb, _, configuration) =>
        {
            // Add chat completion capability
            kb.AddOpenAiChatCompletionServices(builder.Services, builder.Configuration);
        });
        
        builder.Services.AddRefitClient<IOpenAITTSApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://api.openai.com");
                c.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["SemanticKernelOpenAiSettings:ApiKey"]}");
            });

        builder.Services.AddScoped<ITextToSpeechService, OpenAITTSService>();

        return builder;
    }

    /// <summary>
    /// Registers assessments infrastructure.
    /// </summary>
    private static WebApplicationBuilder AddAssessmentInfrastructure(this WebApplicationBuilder builder)
    {
        // Register configs
        builder.Services
            .Configure<GrammarAssessmentGenerationSettings>(builder.Configuration
                .GetSection(nameof(GrammarAssessmentGenerationSettings)))
            .Configure<ListeningAssessmentGenerationSettings>(builder.Configuration
                .GetSection(nameof(ListeningAssessmentGenerationSettings)));
        
        // Register brokers
        builder.Services
            .AddScoped<IQuizAssessmentSessionRepository, QuizAssessmentSessionRepository>()
            .AddScoped<IGrammarAssessmentRepository, GrammarAssessmentRepository>()
            .AddScoped<IListeningAssessmentRepository, ListeningAssessmentRepository>()
            .AddScoped<IQuizQuestionRepository, QuizQuestionRepository>()
            .AddScoped<IQuizOptionRepository, QuizOptionRepository>();

        // Register services
        builder.Services
            .AddScoped<IQuizAssessmentSessionService, QuizAssessmentSessionService>()
            .AddScoped<IGrammarAssessmentService, GrammarAssessmentService>()
            .AddScoped<IListeningAssessmentService, ListeningAssessmentService>()
            .AddScoped<IQuizQuestionService, QuizQuestionService>()
            .AddScoped<IQuizOptionService, QuizOptionService>();

        return builder;
    }
    
    /// <summary>
    /// Configures CORS for the web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        // Register settings
        builder.Services.Configure<CorsSettings>(builder.Configuration.GetSection(nameof(CorsSettings)));
        var corsSettings = builder.Configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>() ??
                           throw new HostAbortedException("Cors settings are not configured");

        builder.Services.AddCors(
            options => options.AddPolicy(
                HostConstants.AllowSpecificOrigins,
                policy =>
                {
                    if (corsSettings.AllowAnyOrigins)
                        policy.AllowAnyOrigin();
                    else
                        policy.WithOrigins(corsSettings.AllowedOrigins);

                    if (corsSettings.AllowAnyHeaders)
                        policy.AllowAnyHeader();

                    if (corsSettings.AllowAnyMethods)
                        policy.AllowAnyMethod();

                    if (corsSettings.AllowCredentials)
                        policy.AllowCredentials();
                }
            )
        );

        return builder;
    }

    /// <summary>
    /// Registers developer tools
    /// </summary>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    /// <summary>
    /// Registers API exposers
    /// </summary>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
        
        builder.Services.Configure<ApiBehaviorOptions>(
            options => { options.SuppressModelStateInvalidFilter = true; }
        );
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services
            .AddControllers()
            .AddNewtonsoftJson();

        return builder;
    }

    /// <summary>
    /// Updates the database schema
    /// </summary>
    private static async ValueTask<WebApplication> UpdateDatabaseAsync(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetRequiredKeyedService<IServiceScopeFactory>(null);

        await serviceScopeFactory.MigrateAsync<AppDbContext>();

        return app;
    }
    
    /// <summary>
    /// Configures CORS settings
    /// </summary>
    private static WebApplication UseCustomCors(this WebApplication app)
    {
        app.UseCors(HostConstants.AllowSpecificOrigins);

        return app;
    }

    /// <summary>
    /// Registers local file storage
    /// </summary>
    private static WebApplication UseLocalFileStorage(this WebApplication app)
    {
        app.UseStaticFiles();

        return app;
    }

    /// <summary>
    /// Registers developer tools middlewares
    /// </summary>
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.MapGet("/", () => "Hi, enjoy AI Capabilities Template API!");

        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    /// <summary>
    /// Registers exposer middlewares
    /// </summary>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}