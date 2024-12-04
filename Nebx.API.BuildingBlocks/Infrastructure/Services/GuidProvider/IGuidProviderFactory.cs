namespace Nebx.API.BuildingBlocks.Infrastructure.Services.GuidProvider;

public interface IGuidProviderFactory
{
    public IGuidProvider Create();
    public IGuidProvider CreateMssql();
}