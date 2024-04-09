using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApp.Business.Common.Authentication;

namespace TutorialApp.WebApi.Areas.Common.Controllers;

/// <summary>
///     This Controller handles all the APIS related to Authentication
/// </summary>
public class AuthenticationController : BaseCommonController
{
    private readonly IAuthService _authService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authService"></param>
    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }


    /// <summary>
    /// FirstName cannot be null value
    /// Lastname can be null value
    /// Email should be of proper format
    /// Password Must be 8 Characters
    /// UserType Must be Applicant
    /// CountryId Must be Greater Than Zero
    /// </summary>
    /// <param name="model">
    /// CreateUserDto
    /// </param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterApplicant([FromBody] CreateUserDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await _authService.CreateUserAsync(model);
        return Ok(response);
    }
    
    /// <summary>
    /// Email should be of proper format
    /// Password is Must
    /// </summary>
    /// <param name="model">
    /// LoginUserDto
    /// </param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("LoginUser")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto model)
    {
        var response = await _authService.LoginUserAsync(model);
        return Ok(response);
    }

    /// <summary>
    /// Send OTP to this API
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("VerifyEmail")]
    public async Task<IActionResult> VerifyEmail([FromBody] OtpDto model)
    {
        var response = await _authService.VerifyEmailAsync(model);
        return Ok(response);
    }

    /// <summary>
    /// Send Email to this API
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("ResendOtp")]
    public async Task<IActionResult> ResendOtp([FromBody] EmailDto model)
    {
        var response = await _authService.ResendOtpAsync(model);
        return Ok(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] EmailDto model)
    {
        var response = await _authService.ForgotPasswordAsync(model);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("VerifyForgotPassword")]
    public async Task<IActionResult> VerifyForgotPassword([FromBody] OtpDto model)
    {
        var response = await _authService.VerifyForgotPasswordAsync(model);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] PasswordDto model)
    {
        var response = await _authService.ResetPasswordAsync(model);
        return Ok(response);
    }
}