using Microsoft.EntityFrameworkCore.Diagnostics;
using Nebx.API.BuildingBlocks.Configurations;
using Nebx.API.BuildingBlocks.Configurations.Interceptors;

namespace Nebx.API.BuildingBlocks;

public static class ServiceConfiguration
{
    public static IServiceCollection AddBuildingBlocksServices(this IServiceCollection services)
    {
        services.AddCors();
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddLoggingConfiguration();
        services.AddQuartzConfiguration();

        services.AddSingleton<TimeProvider>(TimeProvider.System);

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        return services;
    }
}