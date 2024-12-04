using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nebx.API.BuildingBlocks.Application.Configurations;

internal static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerAuthentication();

        //services.ConfigureOptions<SwaggerVersioningOptions>();

        return services;
    }

    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return app;

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

internal class SwaggerVersioningOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerVersioningOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var version in _provider.ApiVersionDescriptions)
        {
            var infoDescription = version.IsDeprecated ? "Deprecated API version." : "";
            options.SwaggerDoc(version.GroupName, CreateVersionInfo(version, infoDescription));
        }
    }

    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription version, string description)
    {
        var openApiInfo = new OpenApiInfo()
        {
            Title = "API Documentation",
            Version = version.ApiVersion.ToString(),
            Description = description,
        };

        return openApiInfo;
    }
}