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
}