using Microsoft.AspNetCore.Identity;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Identity;

namespace TutorialApp.Business.Common.Lookup.UserLookup;

public class UserService : IUserService
{
    private readonly TutorialAppContext _tutorialAppContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    public UserService(
        TutorialAppContext tutorialAppContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager
        )
    {
        _tutorialAppContext = tutorialAppContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<ResponseViewModelGeneric<UserDto>> GetLoggedUserDetailsAsync(string userId,CancellationToken token)
    {
        var existUser = await _userManager.FindByIdAsync(userId);
        if (existUser == null)
        {
            return new ResponseViewModelGeneric<UserDto>()
            {
                StatusCode = 400,
                Success = false,
                Message = "User Not Found"
            };
        }

        var roles = await _userManager.GetRolesAsync(existUser);
        var user = new UserDto
        {
            Id = existUser.Id,
            UserName = existUser.FirstName + " " + existUser.LastName,
            Email = existUser.Email,
            PhoneNumber = existUser.PhoneNumber,
            Role = roles.First()
        };

        return new ResponseViewModelGeneric<UserDto>(user)
        {
            StatusCode = 200,
            Success = true,
            Message = "User Details"
        };
    }
}