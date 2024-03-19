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

        var secretKey = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        TryParse(_configuration["Jwt:TokenValidity"], out var tokenValidity);

        var claims = new List<Claim>
        {
            new("Id", applicationUser.Id),
            new("Name", applicationUser.UserName),
            new("Email", applicationUser.Email),
            new("Roles", userRoles),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescription = new JwtSecurityToken(secretKey, issuer,claims, 
            DateTime.Now, DateTime.Now.AddDays(tokenValidity), 
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescription);
    }
}