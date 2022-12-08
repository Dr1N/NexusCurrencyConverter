using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography;
using Converter.Application.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace Converter.Infrastructure.Authentication;

/// <inheritdoc cref="IJwtHandler" />
public class JwtHandler : IJwtHandler, IDisposable
{
    private readonly JwtSettings settings;
    private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
    
    private SecurityKey issuerSigningKey;
    private SigningCredentials signingCredentials;

    private RSA publicRsa;
    private RSA privateRsa;
    
    public TokenValidationParameters TokenValidationParameters { get; private set; }

    public JwtHandler(JwtSettings settings)
    {
        jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        InitializeRsa();
        InitializeJwtValidationParameters();
    }

    /// <inheritdoc />
    public string GenerateToken(string userName)
    {
        var nowUtc = DateTime.UtcNow;
        var expires = nowUtc.AddMinutes(settings.ExpiryMinutes);
        var centuryBegin = new DateTime(1970, 1, 1);
        var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
        var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
        var issuer = settings.Issuer ?? string.Empty;
        var payload = new JwtPayload
        {
            {"sub", userName},
            {"unique_name", userName},
            {"iss", issuer},
            {"iat", now},
            {"nbf", now},
            {"exp", exp},
            {"jti", Guid.NewGuid().ToString("N")}
        };
        var jwtHeader = new JwtHeader(signingCredentials);
        var jwtSecurityToken = new JwtSecurityToken(jwtHeader, payload);
        var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

        return token;
    }

    public void Dispose()
    {
        publicRsa?.Dispose();
        privateRsa?.Dispose();
    }
    
    private void InitializeRsa()
    {
        publicRsa = RSA.Create();
        var publicKeyXml = File.ReadAllText(settings.RsaPublicKeyXml);
        publicRsa.FromXmlString(publicKeyXml);
        issuerSigningKey = new RsaSecurityKey(publicRsa);

        privateRsa = RSA.Create();
        var privateKeyXml = File.ReadAllText(settings.RsaPrivateKeyXml);
        privateRsa.FromXmlString(privateKeyXml);
        var privateKey = new RsaSecurityKey(privateRsa);
        signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);
    }

    private void InitializeJwtValidationParameters()
    {
        TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidIssuer = settings.Issuer,
            IssuerSigningKey = issuerSigningKey
        }; 
    }
}
    