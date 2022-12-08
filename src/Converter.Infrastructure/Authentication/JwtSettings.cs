// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Converter.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "jwt";
    
    public int ExpiryMinutes { get; set; }
    
    public string Issuer { get; set; }
    
    public string RsaPrivateKeyXml { get; set; }
    
    public string RsaPublicKeyXml { get; set; }
}
