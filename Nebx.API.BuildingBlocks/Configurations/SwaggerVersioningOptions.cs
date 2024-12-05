using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nebx.API.BuildingBlocks.Configurations;

internal sealed class SwaggerVersioningOptions : IConfigureOptions<SwaggerGenOptions>
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