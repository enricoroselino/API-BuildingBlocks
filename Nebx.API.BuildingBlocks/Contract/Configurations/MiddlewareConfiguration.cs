using Microsoft.AspNetCore.Builder;
using Nebx.API.BuildingBlocks.Application.Configurations;

namespace Nebx.API.BuildingBlocks.Contract.Configurations;

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