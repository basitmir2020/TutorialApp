using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Identity;
using TutorialApp.Infrastructure.Models;

namespace TutorialApp.Business.Common.Lookup.ExamTypeLookup;

public class ExamTypeService : IExamTypeService
{
    private readonly TutorialAppContext _tutorialAppContext;
    private readonly UserManager<ApplicationUser> _userManager;
    public ExamTypeService(
        TutorialAppContext tutorialAppContext, 
        UserManager<ApplicationUser> userManager)
    {
        _tutorialAppContext = tutorialAppContext;
        _userManager = userManager;
    }
    public async Task<ResponseViewModelGeneric<List<ExamTypeDto>>> GetAllExamTypeAsync(CancellationToken token,string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ResponseViewModelGeneric<List<ExamTypeDto>>
            {
                StatusCode = 400,
                Success = false,
                Message = "User Not Found"
            };
        }
        
        //Status 
       //1 -> Pending
       //2 -> Active

       var response = await (from examTypes in _tutorialAppContext.ExamTypes
           join aspNetUser in _tutorialAppContext.AspNetUsers on userId equals aspNetUser.Id
           join country in _tutorialAppContext.LkpCountries on aspNetUser.CountryCode equals country.CountryCode
           where examTypes.IsActive && examTypes.StatusId == 2
           select new ExamTypeDto
           {
               Id = examTypes.Id,
               ExamType = examTypes.ExamType1,
               ExamSubType = examTypes.ExamSubType,
               Sequence = examTypes.Sequence
           }).OrderByDescending(x=>x.Sequence)
           .ToListAsync(token);
        if (response.Count > 0)
        {
            return new ResponseViewModelGeneric<List<ExamTypeDto>>(response)
            {
                StatusCode = 200,
                Success = true,
                Message = "List Of Exam Types"
            };
        }
        
        return new ResponseViewModelGeneric<List<ExamTypeDto>>
        {
            StatusCode = 400,
            Success = false,
            Message = "No Exam Type Found"
        };
    }
}