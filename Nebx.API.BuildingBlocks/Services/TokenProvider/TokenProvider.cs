using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nebx.API.BuildingBlocks.Shared.Helpers;

namespace Nebx.API.BuildingBlocks.Services.TokenProvider;

public sealed class TokenProvider : ITokenProvider
{
    public string Scheme => JwtBearerDefaults.AuthenticationScheme;
    public TokenValidationParameters TokenValidationParameters { get; init; }
    private static string Algorithms => SecurityAlgorithms.HmacSha256Signature;
    private readonly IOptions<TokenProviderOptions> _tokenOptions;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly SigningCredentials _signingCredentials;

    public TokenProvider(IOptions<TokenProviderOptions> tokenOptions)
    {
        _tokenOptions = tokenOptions;
        _tokenHandler = new JwtSecurityTokenHandler();

        var saltedKey = CryptoHelper.DerivationKey(tokenOptions.Value.Key, tokenOptions.Value.Salt, 32);
        var symmetricKey = new SymmetricSecurityKey(saltedKey);
        _signingCredentials = new SigningCredentials(symmetricKey, Algorithms);

        TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenOptions.Value.ValidIssuer,
            ValidAudience = tokenOptions.Value.ValidAudience,
            IssuerSigningKey = symmetricKey,
            ClockSkew = TimeSpan.Zero,
        };
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var expiration = DateTime.UtcNow.Add(_tokenOptions.Value.ValidSpan);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _tokenOptions.Value.ValidIssuer,
            Audience = _tokenOptions.Value.ValidAudience,
            SigningCredentials = _signingCredentials,
            Subject = new ClaimsIdentity(claims),
            TokenType = "JWT",
            Expires = expiration,
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = _tokenHandler.WriteToken(token);

        ArgumentException.ThrowIfNullOrWhiteSpace(accessToken, nameof(accessToken));
        return accessToken;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Base64UrlEncoder.Encode(randomNumber);
    }
}