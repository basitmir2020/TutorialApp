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
    /// 
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

    /// <summary>
    /// Get List of Exam Types
    /// </summary>
    /// <param name="token"></param>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("GetExamTypes")]
    public async Task<IActionResult> GetExamTypes(CancellationToken token,string? filter = null, string? orderBy = "Sequence", int? pageNumber = 1, int? pageSize = 10)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examService.GetAllExamTypesAsync(userId,token,filter,orderBy,pageNumber,pageSize);
        return Ok(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="examTypeId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("GetExamTypeById/{examTypeId:int}")]
    public async Task<IActionResult> GetExamTypeById(int examTypeId,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examService.GetExamTypeByIdAsync(userId,examTypeId,token);
        return Ok(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut("UpdateExamType")]
    public async Task<IActionResult> UpdateExamType([FromBody] GetExamType model,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examService.UpdateExamType(userId,model,token);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPut("ChangeStatusExamType")]
    public async Task<IActionResult> ChangeStatusExamType([FromBody] ChangeStatus model,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examService.ChangeStatusAsync(userId,model,token);
        return Ok(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpDelete("DeleteExamType")]
    public async Task<IActionResult> DeleteExamType([FromBody] DeleteExamType model ,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examService.DeleteExamTypeAsync(userId,model,token);
        return Ok(response);
    }
}