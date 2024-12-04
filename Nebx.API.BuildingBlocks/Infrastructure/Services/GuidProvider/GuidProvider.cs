namespace Nebx.API.BuildingBlocks.Infrastructure.Services.GuidProvider;

public class GuidProvider : GuidProviderBase, IGuidProvider
{
    public Guid CreateVersion7() => Guid.CreateVersion7();
}