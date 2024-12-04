using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Nebx.API.BuildingBlocks.Application.Configurations;
using Nebx.API.BuildingBlocks.Infrastructure;
using Nebx.API.BuildingBlocks.Infrastructure.Configurations;
using Nebx.API.BuildingBlocks.Infrastructure.Interceptors;
using Nebx.API.BuildingBlocks.Infrastructure.Services.GuidProvider;

namespace Nebx.API.BuildingBlocks.Contract.Configurations;

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