using UUIDNext;

namespace Nebx.API.BuildingBlocks.Services.GuidProvider;

public sealed class MssqlGuidProvider : GuidProviderBase, IGuidProvider
{
    public Guid CreateVersion7() => Uuid.NewDatabaseFriendly(Database.SqlServer);
}