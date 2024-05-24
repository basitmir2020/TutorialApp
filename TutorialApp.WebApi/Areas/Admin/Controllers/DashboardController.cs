using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApp.Business.Admin.Dashboard;

namespace TutorialApp.WebApi.Areas.Admin.Controllers;


/// <summary>
/// This Controller Contains All The Apis Related to Dashboard
/// </summary>
[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dashboardService"></param>
    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }
    
    /// <summary>
    /// Get All Counts In Dashboard
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("GetAllCounts")]
    public async Task<IActionResult> GetAllCounts(CancellationToken token)
    {
        var response = await _dashboardService.GetAllCountsAsync(token);
        return Ok(response);
    }

}