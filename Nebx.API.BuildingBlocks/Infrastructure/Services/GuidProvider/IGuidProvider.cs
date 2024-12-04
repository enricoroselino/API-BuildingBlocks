namespace Nebx.API.BuildingBlocks.Infrastructure.Services.GuidProvider;

public interface IGuidProvider
{
    public Guid CreateVersion7();
    public Guid NewGuid();
}