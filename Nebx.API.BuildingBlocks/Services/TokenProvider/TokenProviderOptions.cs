using System.ComponentModel.DataAnnotations;

namespace Nebx.API.BuildingBlocks.Services.TokenProvider;

public class TokenProviderOptions
{
    public const string Section = nameof(TokenProviderOptions);
    private const int KeyLength = 32;
    private const int SaltLength = 16;

    [Required(ErrorMessage = "The Key is required.")]
    [StringLength(KeyLength, MinimumLength = KeyLength)]
    public string Key { get; init; } = default!;

    [Required(ErrorMessage = "The Salt is required.")]
    [StringLength(SaltLength, MinimumLength = SaltLength)]
    public string Salt { get; init; } = default!;

    [Required(ErrorMessage = "The ValidIssuer is required.")]
    public string ValidIssuer { get; init; } = default!;

    [Required(ErrorMessage = "The ValidAudience is required.")]
    public string ValidAudience { get; init; } = default!;

    [Required(ErrorMessage = "The ValidSpan is required.")]
    public TimeSpan ValidSpan { get; init; }
}