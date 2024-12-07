using Nebx.API.BuildingBlocks.Configurations;
using Nebx.API.BuildingBlocks.Configurations.Builders;
using Nebx.API.BuildingBlocks.Configurations.ExceptionHandlers;
using Nebx.API.BuildingBlocks.Configurations.Interceptors;
using Serilog;

namespace Nebx.API.BuildingBlocks.PreConfigured;

public static class ServiceConfiguration
{
    public static IServiceCollection AddBuildingBlocksServices(this IServiceCollection services)
    {
        services.AddCors();
        services.AddProblemDetails();
        services.AddExceptionHandler<ErrorLoggerExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        var loggingConfiguration = LoggingConfigurationBuilder.Create();
        var logger = loggingConfiguration.CreateLogger();
        Log.Logger = logger;
        services.AddSerilog(logger);

        services.AddQuartzConfiguration();

        services.AddSingleton<TimeProvider>(TimeProvider.System);

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        return services;
    }
}