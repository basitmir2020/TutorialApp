using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApp.Business.Admin.ExamSubjects;

namespace TutorialApp.WebApi.Areas.Admin.Controllers;

/// <summary>
/// This Controller Contains All The Apis Related to Exam Subjects
/// </summary>
[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("api/[controller]")]
[ApiController]
public class ExamSubjectsController : ControllerBase
{
    private readonly IExamSubjectsService _examSubjectsService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="examSubjectsService"></param>
    public ExamSubjectsController(IExamSubjectsService examSubjectsService)
    {
        _examSubjectsService = examSubjectsService;
    } 
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("SaveExamSubjects")]
    public async Task<IActionResult> SaveExamSubjects([FromBody] ExamSubjectsDto model,CancellationToken token)
    { 
        var userId = User.FindFirstValue("Id");
        var response = await _examSubjectsService.SaveExamSubjectsAsync(model,userId,token);
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
    [HttpGet("GetExamSubjects")]
    public async Task<IActionResult> GetExamSubjects(CancellationToken token,string? filter = null, string? orderBy = "Sequence", int? pageNumber = 1, int? pageSize = 10)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examSubjectsService.GetAllExamTypeSubjectsAsync(userId,token,filter,orderBy,pageNumber,
            pageSize);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="examSubjectId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("GetExamSubjectsById/{examSubjectId:int}")]
    public async Task<IActionResult> GetExamSubjectsById(int examSubjectId,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examSubjectsService.GetExamSubjectsByIdAsync(userId,examSubjectId,token);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut("UpdateExamSubjects")]
    public async Task<IActionResult> UpdateExamSubjects([FromBody] GetExamSubject model,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examSubjectsService.UpdateExamSubjectsAsync(userId,model,token);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPut("ChangeExamSubjectsStatus")]
    public async Task<IActionResult> ChangeStatusExamType([FromBody] ExamSubjectStatus model,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examSubjectsService.ChangeExamSubjectsStatusAsync(userId,model,token);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpDelete("DeleteExamSubjects")]
    public async Task<IActionResult> DeleteExamSubjects([FromBody] DeleteExamSubjects model ,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _examSubjectsService.DeleteExamSubjectsAsync(userId,model,token);
        return Ok(response);
    }
    
      
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("SelectExamSubjects")]
    public async Task<IActionResult> SelectExamSubjects(CancellationToken token)
    {
        var response = await _examSubjectsService.SelectExamSubjectsAsync(token);
        return Ok(response);
    }
}