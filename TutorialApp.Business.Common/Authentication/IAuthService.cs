using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Common.Authentication;

public interface IAuthService
{
    Task<ResponseViewModel> CreateUserAsync(CreateUserDto model);
    Task<TokenResponseViewModel> LoginUserAsync(LoginUserDto model);
    Task<ResponseViewModel> VerifyEmailAsync(OtpDto model);
    Task<ResponseViewModel> ResendOtpAsync(EmailDto model);
    Task<ResponseViewModel> ForgotPasswordAsync(EmailDto model);
    Task<ResponseViewModel> VerifyForgotPasswordAsync(OtpDto model);
    Task<ResponseViewModel> ResetPasswordAsync(PasswordDto model);
}