using Microsoft.AspNetCore.Builder;

namespace Nebx.API.BuildingBlocks.PreConfigured;

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