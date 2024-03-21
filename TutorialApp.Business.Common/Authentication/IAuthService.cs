using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Common.Authentication;

public interface IAuthService
{
    Task<ResponseViewModel> CreateUserAsync(CreateUserDto model);
    Task<TokenResponseViewModel> LoginUserAsync(LoginUserDto model);
}