using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TutorialApp.Infrastructure.Identity;
using static System.Int32;

namespace TutorialApp.Business.Common.Token;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetTokenWithRoles(ApplicationUser applicationUser, string? role)
    {
        var issuer  = _configuration["Jwt:Issuer"];
        var audience= _configuration["Jwt:Audience"];
        var secretKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.FirstName + " " + applicationUser.LastName),
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(ClaimTypes.Role, role),
               
            }),
            Expires = DateTime.UtcNow.AddDays(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}