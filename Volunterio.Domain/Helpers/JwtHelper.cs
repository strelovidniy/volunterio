using Microsoft.IdentityModel.Tokens;
using Volunterio.Domain.Extensions;
using Volunterio.Domain.Settings.Abstraction;

namespace Volunterio.Domain.Helpers;

public static class JwtHelper
{
    public static TokenValidationParameters GetTokenValidationParameters(
        IJwtSettings jwtSettings
    ) => new()
    {
        ValidateIssuer = true,
        ValidIssuers = new[] { jwtSettings.Issuer },

        ValidateAudience = false,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(jwtSettings.TokenSecretString.ToByteArray()),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
}