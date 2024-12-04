using System.Reflection;
using Carter;
using Microsoft.Extensions.DependencyInjection;
using Nebx.API.BuildingBlocks.Shared.Helpers;

namespace Nebx.API.BuildingBlocks.Contract.Extensions;

public static class CarterExtensions
{
    public static IServiceCollection AddCarterFromAssemblies(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddCarter(configurator: Configurator);
        return services;

        void Configurator(CarterConfigurator cfg)
        {
            var modules = AssembliesHelper.GetInterfaceTypes<ICarterModule>(assemblies);
            cfg.WithModules(modules);
        }
    }
}