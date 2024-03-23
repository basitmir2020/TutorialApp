using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApp.Business.Admin.Exams;

namespace TutorialApp.WebApi.Areas.Admin.Controllers;

/// <summary>
/// This Controller Contains All The Apis Related to Exam
/// </summary>
[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("api/[controller]")]
[ApiController]
public class ExamController : ControllerBase
{
    private readonly IExamService _examService;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="examService"></param>
    public ExamController(IExamService examService)
    {
        _examService = examService;
    }

    /// <summary>
    /// Save Exam Types
    /// (Like Medical,Engineering Exams etc)
    /// But Only Admin Can Access This API
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("SaveExamTypes")]
    public async Task<IActionResult> SaveExamTypes([FromBody] ExamTypeDto model,CancellationToken token)
    { 
        var userId = User.FindFirstValue("Id");
        var response = await _examService.SaveExamTypeAsync(model,userId,token);
        return Ok(response);
    }
}