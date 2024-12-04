using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Nebx.API.BuildingBlocks.Infrastructure.Services.TokenProvider;

public interface ITokenProvider
{
    public string Scheme { get; }
    public TokenValidationParameters TokenValidationParameters { get; }
    public string GenerateAccessToken(IEnumerable<Claim> claims);
    public string GenerateRefreshToken();
}