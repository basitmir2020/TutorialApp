using Microsoft.EntityFrameworkCore;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;

namespace TutorialApp.Business.Common.Lookup.UserLookup;

public class UserService : IUserService
{
    private readonly TutorialAppContext _tutorialAppContext;
    public UserService(TutorialAppContext tutorialAppContext)
    {
        _tutorialAppContext = tutorialAppContext;
    }
    public async Task<ResponseViewModelGeneric<UserDto>> GetLoggedUserDetailsAsync(string userId,CancellationToken token)
    {
        var existUser = await _tutorialAppContext.AspNetUsers
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: token);
        if (existUser == null)
        {
            return new ResponseViewModelGeneric<UserDto>()
            {
                StatusCode = 400,
                Success = false,
                Message = "User Not Found"
            };
        }

        var user = new UserDto
        {
            Id = existUser.Id,
            UserName = existUser.FirstName + " " + existUser.LastName,
            Email = existUser.Email
        };

        return new ResponseViewModelGeneric<UserDto>(user)
        {
            StatusCode = 200,
            Success = true,
            Message = "User Details"
        };
    }
}