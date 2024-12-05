using Microsoft.EntityFrameworkCore.Diagnostics;
using Nebx.API.BuildingBlocks.Configurations;
using Nebx.API.BuildingBlocks.Configurations.Interceptors;
using Nebx.API.BuildingBlocks.Services.GuidProvider;

namespace Nebx.API.BuildingBlocks;

public static class ServiceConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddCors()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services
            .AddLoggingConfiguration()
            .AddQuartzConfiguration()
            .AddSwaggerDocumentation();

        services
            .AddAuthenticationConfiguration()
            .AddAuthorization();

        services.AddSingleton<TimeProvider>(TimeProvider.System);

        services
            .AddScoped<ISaveChangesInterceptor, TimeAuditEntityInterceptor>()
            .AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services
            .AddScoped<IGuidProvider, GuidProvider>()
            .AddScoped<IGuidProvider, MssqlGuidProvider>()
            .AddScoped<IGuidProviderFactory, GuidProviderFactory>();

        return services;
    }
}