using TutorialApp.Infrastructure.Identity;

namespace TutorialApp.Business.Common.Token;

public interface ITokenService
{
    string GetTokenWithRoles(ApplicationUser applicationUser, string? role);
    Task<string>  GenerateOtpAsync(string email);
}