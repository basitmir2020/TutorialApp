using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Identity;

namespace TutorialApp.Business.Application.UserExamType;

public class ExamTypeService : IExamTypeService
{
    private readonly TutorialAppContext  _tutorialAppContext;
    private readonly UserManager<ApplicationUser> _userManager;
    public ExamTypeService(TutorialAppContext tutorialAppContext, UserManager<ApplicationUser> userManager)
    {
        _tutorialAppContext = tutorialAppContext;
        _userManager = userManager;
    }
    public async Task<ResponseViewModel> SaveUserExamTypeAsync(UserExamTypeDto model, string userId, CancellationToken token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "User Not Found"
            };
        }

        var examType = await _tutorialAppContext.ExamTypes
            .FirstOrDefaultAsync(x => x.Id == model.ExamTypeId && x.IsActive,token);
        if (examType == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Success = false,
                Message = "Exam Type Not Valid!"
            };
        }

        var userExamType = await _tutorialAppContext.UserExamTypes
            .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive, token);
        if (userExamType != null)
        {
            userExamType.ModifiedOn = DateTime.Now;
            userExamType.ModifiedBy = userId;
            userExamType.IsActive = false;
            _tutorialAppContext.Update(userExamType);
            await _tutorialAppContext.SaveChangesAsync(token);
        }
        
        var response = new Infrastructure.Models.UserExamType
        {
            ExamTypeId = model.ExamTypeId,
            UserId = userId,
            CreatedOn = DateTime.Now,
            CreatedBy = userId,
            ModifiedOn = DateTime.Now,
            ModifiedBy = userId,
            IsActive = true
        };
        await _tutorialAppContext.AddAsync(response, token);
        await _tutorialAppContext.SaveChangesAsync(token);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "User Exam Type Saved Successfully!"
        };
    }
}