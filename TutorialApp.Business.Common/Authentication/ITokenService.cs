using TutorialApp.Infrastructure.Identity;

namespace TutorialApp.Business.Common.Authentication;

public interface ITokenService
{
    string  GetTokenWithRoles(ApplicationUser applicationUser, List<string> roleList);
}