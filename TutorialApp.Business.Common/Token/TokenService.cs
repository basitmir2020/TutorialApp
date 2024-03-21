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

    public string GetTokenWithRoles(ApplicationUser applicationUser, List<string> roleList)
    {
        var userRoles = string.Empty;
        if (roleList is { Count: > 0 })
            userRoles = string.Join(",", roleList);

        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var tryParse = TryParse(_configuration["Jwt:TokenValidity"], out var tokenValidity);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("Id", applicationUser.Id),
            new(JwtRegisteredClaimNames.Name, applicationUser.FirstName + " " + applicationUser.LastName),
            new(JwtRegisteredClaimNames.Sub, applicationUser.Email),
            new(JwtRegisteredClaimNames.Email, applicationUser.Email),
            new("Roles", userRoles)
        };

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(tryParse ? tokenValidity : 5),
            Issuer = issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescription);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}