﻿using Quartz;

namespace Nebx.API.BuildingBlocks.Configurations;

public static class QuartzConfiguration
{
    public static IServiceCollection AddQuartzConfiguration(this IServiceCollection services)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options => { options.WaitForJobsToComplete = true; });
        return services;
    }
}