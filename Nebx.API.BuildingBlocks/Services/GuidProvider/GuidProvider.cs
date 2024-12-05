namespace Nebx.API.BuildingBlocks.Services.GuidProvider;

public sealed class GuidProvider : GuidProviderBase, IGuidProvider
{
    public Guid CreateVersion7() => Guid.CreateVersion7();
}