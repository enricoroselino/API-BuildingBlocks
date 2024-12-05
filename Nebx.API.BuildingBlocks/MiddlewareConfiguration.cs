using Carter;
using Microsoft.AspNetCore.Builder;
using Nebx.API.BuildingBlocks.Configurations;

namespace Nebx.API.BuildingBlocks;

public static class MiddlewareConfiguration
{
    public static IApplicationBuilder UseBuildingBlocksMiddlewares(this WebApplication app)
    {
        app.UseExceptionHandler(_ => { });
        app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseHttpsRedirection();

        app.MapGroup("api").MapCarter();
        return app;
    }
}