using Microsoft.Extensions.DependencyInjection;

namespace Nebx.API.BuildingBlocks.Infrastructure.Services.GuidProvider;

public class GuidProviderFactory : IGuidProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public GuidProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IGuidProvider Create()
    {
        return _serviceProvider.GetRequiredService<GuidProvider>();
    }

    public IGuidProvider CreateMssql()
    {
        return _serviceProvider.GetRequiredService<MssqlGuidProvider>();
    }
}