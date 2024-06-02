using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorialApp.Business.Application.UserExamSubjects;

namespace TutorialApp.WebApi.Areas.Application.Controllers;

/// <summary>
/// 
/// </summary>
[Authorize]
[Area("Applicant")]
[Route("api/[controller]")]
[ApiController]
public class UserExamSubjectsController : ControllerBase
{
    private readonly IUserExamSubjects _userExamSubjects;
    
    /// <summary>
    /// 
    /// </summary>
    public UserExamSubjectsController(IUserExamSubjects userExamSubjects)
    {
        _userExamSubjects = userExamSubjects;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetUserExamSubjects")]
    public async Task<IActionResult> GetUserExamSubject(CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _userExamSubjects.GetAllUserExamSubjectsAsync(userId,token);
        return Ok(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("SaveUserExamSubjects")]
    public async Task<IActionResult> SaveUserExamSubjects([FromBody]SaveUserExamSubjectDto model,CancellationToken token)
    {
        var userId = User.FindFirstValue("Id");
        var response = await _userExamSubjects.SaveUserExamSubjectsAsync(userId,model,token);
        return Ok(response);
    }
}