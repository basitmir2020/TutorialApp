using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApp.Business.Application.ExamType;

namespace TutorialApp.WebApi.Areas.Application.Controllers;

/// <summary>
/// 
/// </summary>
[Authorize]
[Area("Applicant")]
[Route("api/[controller]")]
[ApiController]
public class ExamTypeController : ControllerBase
{
    private readonly IExamTypeService _examTypeService;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="examTypeService"></param>
    public ExamTypeController(IExamTypeService examTypeService)
    {
        _examTypeService = examTypeService;
    }

    /// <summary>
    /// Send Exam Type Id
    /// It will save the User Exam Type
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("SaveUserExamType")]
    public async Task<IActionResult> SaveUserExamType([FromBody] UserExamTypeDto model,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examTypeService.SaveUserExamTypeAsync(model, userId,token);
        return Ok(response);
    }
}