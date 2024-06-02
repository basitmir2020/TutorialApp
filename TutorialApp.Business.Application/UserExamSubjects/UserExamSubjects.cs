using Microsoft.EntityFrameworkCore;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Models;

namespace TutorialApp.Business.Application.UserExamSubjects;

public class UserExamSubjects : IUserExamSubjects
{
    private readonly TutorialAppContext  _tutorialAppContext;

    public UserExamSubjects(TutorialAppContext tutorialAppContext)
    {
        _tutorialAppContext = tutorialAppContext;
    }
    public async  Task<ResponseViewModelGeneric<List<UserExamSubjectDto>>> GetAllUserExamSubjectsAsync(string userId,CancellationToken token)
    {
        var examType = await _tutorialAppContext.UserExamTypes
            .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive == true, cancellationToken: token);
        if (examType == null)
        {
            return new ResponseViewModelGeneric<List<UserExamSubjectDto>>
            {
                Success = false,
                StatusCode = 500,
                Message = "ExamType Not Found!",
            };
        }

        var subjects = await (from x in _tutorialAppContext.ExamSubjects
            where x.ExamTypeId == examType.ExamTypeId && x.IsActive == true && x.StatusId == 2
            orderby x.Sequence descending
            select new UserExamSubjectDto
            {
                Id = x.Id,
                Subject = x.SubjectName
            }).ToListAsync(cancellationToken: token);
        if (subjects.Count <= 0)
        {
            return new ResponseViewModelGeneric<List<UserExamSubjectDto>>
            {
                Success = false,
                Message = "No subjects found for this exam type!"
            };
        }

        return new ResponseViewModelGeneric<List<UserExamSubjectDto>>(subjects)
        {
            Success = true,
            StatusCode = 200,
            Message = "Subject List"
        };
    }

    public async Task<ResponseViewModel> SaveUserExamSubjectsAsync(string userId,SaveUserExamSubjectDto model, CancellationToken token)
    {
        var examType = await _tutorialAppContext.UserExamTypes
            .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive == true, 
                cancellationToken: token);
        if (examType == null)
        {
            return new ResponseViewModel
            {
                Success = false,
                StatusCode = 500,
                Message = "ExamType Not Found!",
            };
        }

        var userSubject = await _tutorialAppContext.UserSubjects
            .Where(x => x.IsActive && x.UserExamTypeId == examType.Id)
            .FirstOrDefaultAsync(cancellationToken: token);
        if (userSubject != null)
        {
            userSubject.IsActive = false;
            userSubject.ModifiedOn = DateTime.Now;
            userSubject.ModifiedBy = userId;
            _tutorialAppContext.Update(userSubject);
            await _tutorialAppContext.SaveChangesAsync(token);
        }

        var saveSubject = new UserSubject
        {
            UserExamTypeId = examType.Id,
            UserSubjectId = model.SubjectId,
            CreatedBy = userId,
            ModifiedBy = userId,
            ModifiedOn = DateTime.Now,
            CreatedOn = DateTime.Now
        };
        await _tutorialAppContext.AddAsync(saveSubject, token);
        await _tutorialAppContext.SaveChangesAsync(token);

        return new ResponseViewModel()
        {
            Success = true,
            Message = "User Exam Subject Saved Successfully!",
            StatusCode = 200
        };
    }
}