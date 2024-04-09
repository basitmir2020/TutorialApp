using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TutorialApp.Business.Common.EmailSending;
using TutorialApp.Business.Common.Token;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Identity;

namespace TutorialApp.Business.Common.Authentication;

public class AuthService : IAuthService
{
    private readonly TutorialAppContext _tutorialAppContext;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> roleManager,
        ITokenService tokenService, IEmailService emailService, TutorialAppContext tutorialAppContext)
    {
        _tokenService = tokenService;
        _emailService = emailService;
        _tutorialAppContext = tutorialAppContext;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<ResponseViewModel> CreateUserAsync(CreateUserDto model)
    {
        var existUser = await _userManager.FindByEmailAsync(model.Email);
        if (existUser != null)
            return new ResponseViewModel
            {
                Success = false,
                StatusCode = 400,
                Message = "User already exists with this email!"
            };

        var user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.Email,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            CountryCode = model.CountryCode,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            EmailConfirmed = false
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            foreach (var error in result.Errors)
                return new ResponseViewModel
                {
                    StatusCode = 400,
                    Success = false,
                    Message = error.Description
                };

        if (model.UserType == "Applicant")
        {
            var role = await _roleManager.FindByIdAsync("1");
            if (role != null)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
        }
        else
        {
            var role = await _roleManager.FindByIdAsync("2");
            if (role != null)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
        }

        //Otp Type is 1 because it is for Account Verification
        var otp = await _tokenService.GenerateOtpAsync(user.Id, 1);
        var body = "" +
                   "<!DOCTYPE html>\n" +
                   "<html lang=\"en\">\n" +
                   "<head>\n   " +
                   " <meta charset=\"UTF-8\">\n   " +
                   " <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    " +
                   "<title>OTP Verification</title>\n" +
                   "</head>\n" +
                   "<body style=\"font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;\">\n    " +
                   "<div style=\"width: 100%; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\">\n      " +
                   "<div style=\"text-align: center; margin-bottom: 30px;\">\n" +
                   "<h1 style=\"color: #333;\">OTP Verification</h1>\n        " +
                   "</div>\n" +
                   "<div style=\"padding: 20px; background-color: #f9f9f9; border-radius: 5px;\">\n            " +
                   "<p>Dear " + char.ToUpper(user.FirstName[0]) + user.FirstName[1..] + " " +
                   char.ToUpper(user.LastName[0]) + user.LastName[1..] + ",</p>\n           " +
                   " <p>Your OTP code for registration is:</p>\n            " +
                   "<h2 style=\"text-align: center; color: #007bff;\"> " + otp + " </h2>\n            " +
                   "<p>Please use this code to verify your email within 2 days.</p>\n      " +
                   "<p style=\"font-size: 12px; color: #777; text-align: center;\">This is an automated email. Please do not reply.</p> " +
                   " </div>\n        " +
                   "<div style=\"margin-top: 30px; text-align: center;\">\n            " +
                   "<p>Thank you for using our application.</p>\n        " +
                   "</div>\n    " +
                   "</div>\n</body>\n</html>\n";
        await _emailService.SendEmailAsync(user.Email, "Confirm Your Email Address", body);

        return new ResponseViewModel
        {
            Success = true,
            StatusCode = 200,
            Message = "User Registered Successfully!"
        };
    }

    public async Task<TokenResponseViewModel> LoginUserAsync(LoginUserDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return new TokenResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "User not registered!",
            };
        }

        var isEmailConfirmedAsync = await _userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmedAsync)
        {
            return new TokenResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "Email is not verified!"
            };
        }

        var checkAuthenticUser = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!checkAuthenticUser)
        {
            return new TokenResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "Invalid credentials!"
            };
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GetTokenWithRoles(user, roles.FirstOrDefault());

        return new TokenResponseViewModel
        {
            Success = true,
            StatusCode = 200,
            Message = "User signed successfully",
            Token = token
        };
    }

    public async Task<ResponseViewModel> VerifyEmailAsync(OtpDto model)
    {
        var existOtp = await _tutorialAppContext.AspNetUserOtp
            .FirstOrDefaultAsync(
                x => x.Otp == model.Otp &&
                     x.OtpType == 1 &&
                     x.IsActive == true && x.StatusId == 1);
        if (existOtp == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "This OTP is not valid!"
            };
        }

        if (DateTime.Now > existOtp.Expiry)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "This OTP is already expired!"
            };
        }

        existOtp.StatusId = 2;
        existOtp.ModifiedOn = DateTime.Now;
        _tutorialAppContext.Update(existOtp);
        await _tutorialAppContext.SaveChangesAsync();
        var user = await _userManager.FindByIdAsync(existOtp.UserId);
        user.EmailConfirmed = true;
        user.ModifiedDate = DateTime.Now;
        await _userManager.UpdateAsync(user);
        return new ResponseViewModel
        {
            Success = true,
            StatusCode = 200,
            Message = "Email verified successfully!"
        };
    }

    public async Task<ResponseViewModel> ResendOtpAsync(EmailDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "Email not valid!"
            };
        }

        if (user.EmailConfirmed)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "Email already confirmed!"
            };
        }

        var newOtp = await _tokenService.GenerateOtpAsync(user.Id, 1);

        var body = "" +
                   "<!DOCTYPE html>\n" +
                   "<html lang=\"en\">\n" +
                   "<head>\n   " +
                   " <meta charset=\"UTF-8\">\n   " +
                   " <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    " +
                   "<title>OTP Verification</title>\n" +
                   "</head>\n" +
                   "<body style=\"font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;\">\n    " +
                   "<div style=\"width: 100%; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\">\n      " +
                   "<div style=\"text-align: center; margin-bottom: 30px;\">\n" +
                   "<h1 style=\"color: #333;\">OTP Verification</h1>\n        " +
                   "</div>\n" +
                   "<div style=\"padding: 20px; background-color: #f9f9f9; border-radius: 5px;\">\n            " +
                   "<p>Dear " + char.ToUpper(user.FirstName[0]) + user.FirstName[1..] + " " +
                   char.ToUpper(user.LastName[0]) + user.LastName[1..] + ",</p>\n           " +
                   " <p>Your OTP code for registration is:</p>\n            " +
                   "<h2 style=\"text-align: center; color: #007bff;\"> " + newOtp + " </h2>\n            " +
                   "<p>Please use this code to verify your email within 2 days.</p>\n      " +
                   "<p style=\"font-size: 12px; color: #777; text-align: center;\">This is an automated email. Please do not reply.</p> " +
                   " </div>\n        " +
                   "<div style=\"margin-top: 30px; text-align: center;\">\n            " +
                   "<p>Thank you for using our application.</p>\n        " +
                   "</div>\n    " +
                   "</div>\n</body>\n</html>\n";
        await _emailService.SendEmailAsync(user.Email, "Confirm Your Email Address", body);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Otp send to email!"
        };
    }

    public async Task<ResponseViewModel> ForgotPasswordAsync(EmailDto model)
    {
        var existUser = await _userManager.FindByEmailAsync(model.Email);
        if (existUser == null)
        {
            return new ResponseViewModel
            {
                Success = false,
                StatusCode = 400,
                Message = "User Not Registered!"
            };
        }

        var forgotOtp = await _tokenService.GenerateOtpAsync(existUser.Id, 2);
        var body = "" +
                   "<!DOCTYPE html>\n" +
                   "<html lang=\"en\">\n" +
                   "<head>\n   " +
                   " <meta charset=\"UTF-8\">\n   " +
                   " <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    " +
                   "<title>Forgot Password</title>\n" +
                   "</head>\n" +
                   "<body style=\"font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;\">\n    " +
                   "<div style=\"width: 100%; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\">\n      " +
                   "<div style=\"text-align: center; margin-bottom: 30px;\">\n" +
                   "<h1 style=\"color: #333;\">Forgot Password</h1>\n        " +
                   "</div>\n" +
                   "<div style=\"padding: 20px; background-color: #f9f9f9; border-radius: 5px;\">\n            " +
                   "<p>Dear " + char.ToUpper(existUser.FirstName[0]) + existUser.FirstName[1..] + " " +
                   char.ToUpper(existUser.LastName[0]) + existUser.LastName[1..] + ",</p>\n           " +
                   " <p>Your OTP code for forgot password is:</p>\n            " +
                   "<h2 style=\"text-align: center; color: #007bff;\"> " + forgotOtp + " </h2>\n            " +
                   "<p>Please use this code to verify your email within 2 days.</p>\n      " +
                   "<p style=\"font-size: 12px; color: #777; text-align: center;\">This is an automated email. Please do not reply.</p> " +
                   " </div>\n        " +
                   "<div style=\"margin-top: 30px; text-align: center;\">\n            " +
                   "<p>Thank you for using our application.</p>\n        " +
                   "</div>\n    " +
                   "</div>\n</body>\n</html>\n";
        await _emailService.SendEmailAsync(existUser.Email, "Forgot Password", body);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Forgot Password Otp send to email!"
        };
    }

    public async Task<ResponseViewModel> VerifyForgotPasswordAsync(OtpDto model)
    {
        var existOtp = await _tutorialAppContext.AspNetUserOtp
            .FirstOrDefaultAsync(x =>
                x.Otp == model.Otp &&
                x.IsActive == true &&
                x.OtpType == 2 && x.StatusId == 1);
        if (existOtp == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "This OTP is not valid!"
            };
        }
        if (DateTime.Now > existOtp.Expiry)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "This OTP is already expired!"
            };
        }

        existOtp.StatusId = 2;
        existOtp.ModifiedOn = DateTime.Now;
        _tutorialAppContext.AspNetUserOtp.Update(existOtp);
        await _tutorialAppContext.SaveChangesAsync();

        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Verified Successfully!"
        };
    }

    public async Task<ResponseViewModel> ResetPasswordAsync(PasswordDto model)
    {
        var existUser = await _userManager.FindByEmailAsync(model.Email);
        if (existUser == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "User is not valid!"
            };
        }
        
        var existOtp = await _tutorialAppContext.AspNetUserOtp
            .FirstOrDefaultAsync(x =>
                x.UserId == existUser.Id &&
                x.IsActive == true &&
                x.OtpType == 2 &&
                x.StatusId == 2);
        if (existOtp == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "Forgot Password otp not verified!"
            };
        }

        existOtp.IsActive = false;
        existOtp.ModifiedOn = DateTime.Now;
        _tutorialAppContext.Update(existOtp);
        await _tutorialAppContext.SaveChangesAsync();
        
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
        await _userManager.ResetPasswordAsync(existUser, token, model.NewPassword);
        
        return new ResponseViewModel
        {
            Success = true,
            StatusCode = 200,
            Message = "Password Reset Successfully!"
        };

    }
}