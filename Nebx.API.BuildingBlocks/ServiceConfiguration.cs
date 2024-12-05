using Microsoft.EntityFrameworkCore.Diagnostics;
using Nebx.API.BuildingBlocks.Configurations;
using Nebx.API.BuildingBlocks.Configurations.Interceptors;
using Nebx.API.BuildingBlocks.Services.GuidProvider;

namespace Nebx.API.BuildingBlocks;

public static class ServiceConfiguration
{
    public static IServiceCollection AddBuildingBlocksService(this IServiceCollection services)
    {
        services.AddCors();
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddLoggingConfiguration();
        services.AddQuartzConfiguration();

        services.AddSingleton<TimeProvider>(TimeProvider.System);

        services.AddScoped<ISaveChangesInterceptor, TimeAuditEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddScoped<IGuidProvider, GuidProvider>();
        services.AddScoped<IGuidProvider, MssqlGuidProvider>();
        services.AddScoped<IGuidProviderFactory, GuidProviderFactory>();

        return services;
    }
}