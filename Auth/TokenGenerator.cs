using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ToDoApp.Auth;

public class TokenGenerator
{
    private readonly JwtSettings _settings;

    public TokenGenerator(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }

    public string Generate(string email)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, email),
            new(ClaimTypes.Role, "api-user"),
            new("test type", "test value")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecrectKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _settings.Issuer,
            _settings.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credentials);

        var tokenGenerator = new JwtSecurityTokenHandler();
        var jwtString = tokenGenerator.WriteToken(token);
        return jwtString;
    }
}