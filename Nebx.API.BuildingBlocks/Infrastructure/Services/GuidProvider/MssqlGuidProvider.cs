using UUIDNext;

namespace Nebx.API.BuildingBlocks.Infrastructure.Services.GuidProvider;

public class MssqlGuidProvider : GuidProviderBase, IGuidProvider
{
    public Guid CreateVersion7() => Uuid.NewDatabaseFriendly(Database.SqlServer);
}