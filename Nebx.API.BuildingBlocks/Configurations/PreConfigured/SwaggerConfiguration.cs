using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Nebx.API.BuildingBlocks.Configurations.PreConfigured;

internal static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        
        // TODO: client might use another type of authentication method
        services.AddSwaggerAuthentication();

        //services.ConfigureOptions<SwaggerVersioningOptions>();

        return services;
    }

    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();


        // app.UseSwaggerUI(options =>
        // {
        //     foreach (var description in app.DescribeApiVersions())
        //     {
        //         var url = $"/swagger/{description.GroupName}/swagger.json";
        //         var name = description.GroupName.ToUpperInvariant();
        //         options.SwaggerEndpoint(url, name);
        //     }
        // });

        return app;
    }

    private static IServiceCollection AddSwaggerAuthentication(this IServiceCollection services)
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            In = ParameterLocation.Header,
            BearerFormat = "JWT",
            Scheme = "bearer",
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            },
            Description = "Please enter your JWT with this format: ''YOUR_TOKEN''",
        };

        var securityRequirement = new OpenApiSecurityRequirement { { securityScheme, Array.Empty<string>() } };

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", securityScheme);
            options.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}