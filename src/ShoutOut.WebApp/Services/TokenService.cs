using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ShoutOut.WebApp.Services;

public interface ITokenService
{
    string CreateToken(string userEmail);
}

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string CreateToken(string userEmail)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Email, userEmail));
        
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            configuration["Token"] ?? throw new InvalidOperationException()));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}