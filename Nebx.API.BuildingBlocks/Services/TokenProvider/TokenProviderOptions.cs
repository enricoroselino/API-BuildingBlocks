namespace Nebx.API.BuildingBlocks.Services.TokenProvider;

public class TokenProviderOptions
{
    public const string Section = nameof(TokenProviderOptions);
    public string Key { get; init; } = default!;
    public string Salt { get; init; } = default!;
    public string ValidIssuer { get; init; } = default!;
    public string ValidAudience { get; init; } = default!;
    public TimeSpan ValidSpan { get; init; }
}