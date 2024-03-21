using Microsoft.AspNetCore.Mvc;

namespace TutorialApp.WebApi.Areas.Common.Controllers;

/// <summary>
///     This Controller acts as the base for our common controllers
/// </summary>
[Area("Common")]
[Route("api/[controller]")]
[ApiController]
public class BaseCommonController : ControllerBase
{
}