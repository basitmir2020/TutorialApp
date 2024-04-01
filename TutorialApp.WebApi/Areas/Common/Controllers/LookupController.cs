using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApp.Business.Common.Lookup.CountryLookup;
using TutorialApp.Business.Common.Lookup.ExamTypeLookup;
using TutorialApp.Business.Common.Lookup.UserLookup;

namespace TutorialApp.WebApi.Areas.Common.Controllers;

/// <summary>
///     This Controller handles all the APIS related to Common Lookup Values
/// </summary>
[Authorize]
public class LookupController : BaseCommonController
{
    private readonly ICountryService _countryService;
    private readonly IExamTypeService _examTypeService;
    private readonly IUserService _userService;

    /// <summary>
    ///     This Controller handles all the APIS related to Common Lookup Values
    /// </summary>
    public LookupController(
        ICountryService countryService,
        IExamTypeService examTypeService,
        IUserService userService
        )
    {
        _countryService = countryService;
        _examTypeService = examTypeService;
        _userService = userService;
    }


    /// <summary>
    ///     This API will return you List Of Countries
    /// </summary>
    /// <returns>
    ///     Countries DTO
    ///     ID
    ///     CountryName
    ///     CountryCode
    /// </returns>
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("GetCountries")]
    public async Task<IActionResult> GetCountries()
    {
        var response = await _countryService.GetCountriesAsync();
        return Ok(response);
    }
    
    /// <summary>
    ///  This API will return you List Of Exam Types
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("GetExamTypes")]
    public async Task<IActionResult> GetExamTypes(CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examTypeService.GetAllExamTypeAsync(token, userId);
        return Ok(response);
    }
    
    /// <summary>
    /// This will return the Logged In User Details
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("GetLoggedUser")]
    public async Task<IActionResult> GetLoggedUser(CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _userService.GetLoggedUserDetailsAsync(userId,token);
        return Ok(response);
    }
}