using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Identity;

namespace TutorialApp.Business.Common.Token;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly TutorialAppContext _tutorialAppContext;

    public TokenService(IConfiguration configuration, TutorialAppContext tutorialAppContext)
    {
        _configuration = configuration;
        _tutorialAppContext = tutorialAppContext;
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

    public async Task<string> GenerateOtpAsync(string userId)
    {
        var otp = GenerateRandomOtp();
        var userOtp = await _tutorialAppContext
            .AspNetUserOtp
            .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive == true);
        if (userOtp != null)
        {
            userOtp.IsActive = false;
            userOtp.ModifiedOn = DateTime.Now;
            userOtp.ModifiedBy = userId;
            _tutorialAppContext.AspNetUserOtp.Update(userOtp);
           await _tutorialAppContext.SaveChangesAsync();
        }
        var saveOtp = new AspNetUserOtp
        {
            UserId = userId,
            Otp = otp,
            Expiry = DateTime.Now.AddDays(2),
            StatusId = 1,
            IsActive = true,
            CreatedBy = userId,
            CreatedOn = DateTime.Now,
            ModifiedBy = userId,
            ModifiedOn = DateTime.Now
        };
        await _tutorialAppContext.AspNetUserOtp.
            AddAsync(saveOtp);
        await _tutorialAppContext.SaveChangesAsync();
        return otp;
    }
    
    private static string GenerateRandomOtp()
    {
        const string chars = "0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}