namespace Nebx.API.BuildingBlocks.Services.GuidProvider;

public interface IGuidProvider
{
    public Guid CreateVersion7();
    public Guid NewGuid();
}