using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infra;
using Microsoft.IdentityModel.Tokens;

namespace Service.Security;

public class JwtTokenService(JwtSettings settings) : ITokenService
{
    public string CreateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(settings.Key));
        
        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: settings.Issuer,
            audience: settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(settings.ExpiresMinutes),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}