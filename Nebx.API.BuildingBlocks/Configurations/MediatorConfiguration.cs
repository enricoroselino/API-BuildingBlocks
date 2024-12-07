using Nebx.API.BuildingBlocks.Configurations.Behaviors;

namespace Nebx.API.BuildingBlocks.Configurations;

public static class MediatorConfiguration
{
    public static IServiceCollection AddMediatorFromAssemblies(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        // validation added as part of mediator
        services.AddValidatorsFromAssemblies(assemblies);
        return services;
    }
}