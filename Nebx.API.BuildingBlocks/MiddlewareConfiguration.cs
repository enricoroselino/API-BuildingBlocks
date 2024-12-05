using Carter;
using Microsoft.AspNetCore.Builder;
using Nebx.API.BuildingBlocks.Configurations;
using Nebx.API.BuildingBlocks.Configurations.PreConfigured;

namespace Nebx.API.BuildingBlocks;

public static class MiddlewareConfiguration
{
    public static IApplicationBuilder UseBuildingBlocksMiddleware(this WebApplication app)
    {
        app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseHttpsRedirection();

        app.UseSwaggerDocumentation();
        app.MapGroup("api").MapCarter();
        return app;
    }
}