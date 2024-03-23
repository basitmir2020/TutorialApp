using Microsoft.AspNetCore.Identity;
using TutorialApp.Business.Common.Token;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Identity;

namespace TutorialApp.Business.Common.Authentication;

public class AuthService : IAuthService
{
    private readonly TutorialAppContext _identityContext;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(
        TutorialAppContext identityContext,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> roleManager,
        ITokenService tokenService)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _identityContext = identityContext;
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
            ExamTypeId = model.ExamTypeId,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            EmailConfirmed = true
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
                Message = "User Not Registered!",
            };
        }

        var checkAuthenticUser = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!checkAuthenticUser)
        {
            return new TokenResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "Invalid Credentials!"
            };
        }

        var roles = await _userManager.GetRolesAsync(user);
        Console.WriteLine(roles);
        var token =  _tokenService.GetTokenWithRoles(user, roles.FirstOrDefault());

        return new TokenResponseViewModel
        {
            Success = true,
            StatusCode = 200,
            Message = "User Signed Successfully",
            Token = token
        };
    }
}