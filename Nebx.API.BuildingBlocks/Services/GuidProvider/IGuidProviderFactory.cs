namespace Nebx.API.BuildingBlocks.Services.GuidProvider;

public interface IGuidProviderFactory
{
    public IGuidProvider Create();
    public IGuidProvider CreateMssql();
}