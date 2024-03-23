using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Common.Lookup.UserLookup;

public interface IUserService
{
    Task<ResponseViewModelGeneric<UserDto>> GetLoggedUserDetailsAsync(string userId,CancellationToken token);
}