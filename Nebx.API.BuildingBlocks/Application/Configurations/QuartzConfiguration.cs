using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Nebx.API.BuildingBlocks.Application.Configurations;

public static class QuartzConfiguration
{
    public static IServiceCollection AddQuartzConfiguration(this IServiceCollection services)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options => { options.WaitForJobsToComplete = true; });
        return services;
    }
}