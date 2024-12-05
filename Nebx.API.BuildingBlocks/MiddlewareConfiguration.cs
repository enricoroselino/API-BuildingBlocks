using Microsoft.AspNetCore.Builder;
using Nebx.API.BuildingBlocks.Configurations;

namespace Nebx.API.BuildingBlocks;

public static class MiddlewareConfiguration
{
    public static IApplicationBuilder UseApplicationMiddlewares(this WebApplication app)
    {
        app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
            .UseHttpsRedirection()
            .UseAuthentication()
            .UseAuthorization();

        app.UseSwaggerDocumentation();
        return app;
    }
}