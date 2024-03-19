using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApp.Business.Common.Lookup.CountryLookup;

namespace TutorialApp.WebApi.Areas.Common.Controllers;

/// <summary>
/// This Controller handles all the APIS related to Common Lookup Values
/// </summary>
public class LookupController : BaseCommonController
{
    private readonly ICountryService _countryService;
    
    /// <summary>
    /// This Controller handles all the APIS related to Common Lookup Values
    /// </summary>
    public LookupController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    
    /// <summary>
    /// This API will return you List Of Countries
    /// </summary>
    /// <returns>
    /// Countries DTO
    /// ID
    /// CountryName
    /// CountryCode
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
}