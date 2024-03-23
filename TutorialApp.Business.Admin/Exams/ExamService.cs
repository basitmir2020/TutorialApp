using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Models;

namespace TutorialApp.Business.Admin.Exams;

public class ExamService : IExamService
{
    private readonly TutorialAppContext _tutorialAppContext;
    public ExamService(TutorialAppContext tutorialAppContext)
    {
        _tutorialAppContext = tutorialAppContext;
    }
    public async Task<ResponseViewModel> SaveExamTypeAsync(ExamTypeDto model,string userId,CancellationToken token)
    {
        var examTypes = await _tutorialAppContext.LkpExamTypes
            .FirstOrDefaultAsync(x => x.ExamType == model.ExamType && x.IsActive == true, cancellationToken: token);
        if (examTypes != null)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Message = "Exam Type Already Created!",
                Success = false
            };
        }

        var examTypesCount = await _tutorialAppContext
            .LkpExamTypes
            .ToListAsync(cancellationToken: token);
        var newExamTypes = new LkpExamTypes
        {
            ExamType = model.ExamType,
            ExamTypeIcon = model.ExamTypeIcon,
            IsActive = true,
            Status = "Pending",
            Sequence = examTypesCount.Count + 1,
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            CreatedBy = userId,
            ModifiedBy = userId
        };

        await _tutorialAppContext.AddAsync(newExamTypes, token);
        await _tutorialAppContext.SaveChangesAsync(token);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Message = "Exam Type Saved Successfully!",
            Success = true
        };
    }
}