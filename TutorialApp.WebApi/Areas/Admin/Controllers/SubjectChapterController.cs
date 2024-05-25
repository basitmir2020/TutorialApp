using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApp.Business.Admin.SubjectChapters;

namespace TutorialApp.WebApi.Areas.Admin.Controllers;

/// <summary>
/// This Controller Contains All The Apis Related to Subject Chapters
/// </summary>
[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("api/[controller]")]
[ApiController]
public class SubjectChapterController : ControllerBase
{
    private readonly ISubjectChapters _subjectChapters;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="subjectChapters"></param>
    public SubjectChapterController(ISubjectChapters subjectChapters)
    {
        _subjectChapters = subjectChapters;
    } 
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("SaveSubjectChapters")]
    public async Task<IActionResult> SaveSubjectChapters([FromBody] SubjectChaptersDto model,CancellationToken token)
    { 
        var userId = User.FindFirstValue("Id");
        var response = await _subjectChapters.SaveSubjectChaptersAsync(model,userId,token);
        return Ok(response);
    }

    /// <summary>
    /// Get List of Subject Chapters
    /// </summary>
    /// <param name="token"></param>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("GetSubjectChapters")]
    public async Task<IActionResult> GetSubjectChapters(CancellationToken token,string? filter = null, string? orderBy = "Sequence", int? pageNumber = 1, int? pageSize = 10)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _subjectChapters.GetAllSubjectChaptersAsync(userId,token,filter,orderBy,pageNumber,
            pageSize);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="subjectChapterId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("GetSubjectChaptersById/{subjectChapterId:int}")]
    public async Task<IActionResult> GetSubjectChaptersById(int subjectChapterId,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _subjectChapters.GetSubjectChaptersByIdAsync(userId,subjectChapterId,token);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut("UpdateSubjectChapters")]
    public async Task<IActionResult> UpdateSubjectChapters([FromBody] GetSubjectChapters model,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _subjectChapters.UpdateSubjectChaptersAsync(userId,model,token);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPut("ChangeSubjectChaptersStatus")]
    public async Task<IActionResult> ChangeSubjectChaptersStatus([FromBody] SubjectChapterStatus model,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _subjectChapters.ChangeSubjectChaptersStatusAsync(userId,model,token);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpDelete("DeleteSubjectChapters")]
    public async Task<IActionResult> DeleteSubjectChapters([FromBody] DeleteSubjectChapters model ,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _subjectChapters.DeleteSubjectChaptersAsync(userId,model,token);
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("SelectSubjectChapters")]
    public async Task<IActionResult> SelectSubjectChapters(CancellationToken token)
    {
        var response = await _subjectChapters.SelectSubjectChaptersAsync(token);
        return Ok(response);
    }

}