using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Identity;
using static System.Int32;

namespace TutorialApp.Business.Common.Authentication;

public class TokenService : ITokenService
{

    private readonly TutorialAppIdentityContext _identityContext;
    private readonly TutorialAppContext _tutorialAppContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    public TokenService(
        TutorialAppIdentityContext identityContext,
        TutorialAppContext tutorialAppContext,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _identityContext = identityContext;
        _tutorialAppContext = tutorialAppContext;
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public string GetTokenWithRoles(ApplicationUser applicationUser, List<string> roleList)
    {
        var userRoles = string.Empty;
        if (roleList != null && roleList.Count > 0)
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