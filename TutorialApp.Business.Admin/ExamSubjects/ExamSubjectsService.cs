using Microsoft.EntityFrameworkCore;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Models;

namespace TutorialApp.Business.Admin.ExamSubjects;

public class ExamSubjectsService : IExamSubjectsService
{
    private readonly TutorialAppContext _tutorialAppContext;

    public ExamSubjectsService(TutorialAppContext tutorialAppContext)
    {
        _tutorialAppContext = tutorialAppContext;
    }
    public async Task<ResponseViewModel> SaveExamSubjectsAsync(ExamSubjectsDto model, string userId, CancellationToken token)
    {
        var examTypes = await _tutorialAppContext.ExamSubjects
            .FirstOrDefaultAsync(x => x.ExamTypeId == model.ExamTypeId &&
                                      x.SubjectName == model.SubjectName &&
                                      x.IsActive == true, cancellationToken: token);
        if (examTypes != null)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Message = "Exam Subjects Already Created!",
                Success = false
            };
        }
        
        var examSubjects = await _tutorialAppContext
            .ExamSubjects
            .ToListAsync(cancellationToken: token);
        var newExamSubject = new ExamSubject
        {
            ExamTypeId = model.ExamTypeId,
            SubjectName = model.SubjectName,
            IsActive = true,
            StatusId = 1,
            Sequence = examSubjects.Count + 1,
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            CreatedBy = userId,
            ModifiedBy = userId
        };
        
        await _tutorialAppContext.AddAsync(newExamSubject, token);
        await _tutorialAppContext.SaveChangesAsync(token);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Message = "Exam Subject Saved Successfully!",
            Success = true
        };
    }

    public async Task<ResponseViewModelGeneric<GetExamSubject>> GetExamSubjectsByIdAsync(string userId, int examSubjectId, CancellationToken token)
    {
        var existExamSubject = await _tutorialAppContext.ExamSubjects
            .FirstOrDefaultAsync(x => x.Id == examSubjectId,token);
        if (existExamSubject == null)
        {
            return new ResponseViewModelGeneric<GetExamSubject>
            {
                StatusCode = 500,
                Success = false,
                Message = "Exam Subject Not Found!",
            };
        }

        var response = new GetExamSubject
        {
            Id = existExamSubject.Id,
            ExamTypeId = Convert.ToInt32(existExamSubject.ExamTypeId),
            SubjectsName = existExamSubject.SubjectName,
            ExamType = _tutorialAppContext.ExamTypes.Where(x=>x.Id == existExamSubject.ExamTypeId)
                .Select(x=>x.ExamType1).FirstOrDefault()
        };

        return new ResponseViewModelGeneric<GetExamSubject>(response)
        {
            StatusCode = 200,
            Success = true,
            Message = "Exam Subjects Data!"
        };
    }

    public async Task<ResponseViewModel> UpdateExamSubjectsAsync(string userId, GetExamSubject model, CancellationToken token)
    {
        var examSubject = await _tutorialAppContext.ExamSubjects
            .FirstOrDefaultAsync(x => x.Id == model.Id,token);
        if (examSubject == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 500,
                Success = false,
                Message = "Exam Subject Not Found!",
            };
        }

        examSubject.ExamTypeId = model.ExamTypeId;
        examSubject.SubjectName = model.SubjectsName;
        examSubject.ModifiedOn = DateTime.Now;
        examSubject.ModifiedBy = userId;
        _tutorialAppContext.Update(examSubject);
        await _tutorialAppContext.SaveChangesAsync(token);

        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Exam Subjects Updated Successfully!"
        };
    }

    public async Task<ResponseViewModel> ChangeExamSubjectsStatusAsync(string userId, ExamSubjectStatus model, CancellationToken token)
    {
        var examSubject = await _tutorialAppContext.ExamSubjects
            .FirstOrDefaultAsync(x => x.Id == model.ExamSubjectsId,token);
        if (examSubject == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 500,
                Success = false,
                Message = "Exam Subjects Not Found!",
            };
        }

        examSubject.StatusId = model.StatusId;
        examSubject.ModifiedOn = DateTime.Now;
        examSubject.ModifiedBy = userId;
        _tutorialAppContext.Update(examSubject);
        await _tutorialAppContext.SaveChangesAsync(token);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Status Changed Successfully!",
        };
    }

    public async Task<ResponseViewModel> DeleteExamSubjectsAsync(string userId, DeleteExamSubjects model, CancellationToken token)
    {
        var examSubject = await _tutorialAppContext.ExamSubjects
            .FirstOrDefaultAsync(x => x.Id == model.ExamSubjectsId,token);
        if (examSubject == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 500,
                Success = false,
                Message = "Exam Subjects Not Found!",
            };
        }

        examSubject.IsActive =false;
        examSubject.ModifiedOn = DateTime.Now;
        examSubject.ModifiedBy = userId;
        _tutorialAppContext.Update(examSubject);
        await _tutorialAppContext.SaveChangesAsync(token);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Exam Subjects deleted successfully!",
        };
    }

    public async Task<ResponseViewModelGeneric<List<ExamSubjectsVM>>> SelectExamSubjectsAsync(CancellationToken token)
    {
        var examSubjects = await (from examSubject in _tutorialAppContext.ExamSubjects
            join examTypes in _tutorialAppContext.ExamTypes on examSubject.ExamTypeId equals examTypes.Id 
            where examSubject.IsActive
            select new ExamSubjectsVM
            {
                Id = examSubject.Id,
                SubjectName = examSubject.SubjectName + " (" + examTypes.ExamType1 + ")"
            }).ToListAsync(cancellationToken: token);
        return new ResponseViewModelGeneric<List<ExamSubjectsVM>>(examSubjects)
        {
            Success = true,
            StatusCode = 200,
            Message = "Select Exam Subjects"
        };
    }

    public async Task<ResponseViewModelGeneric<List<GetAllExamTypeSubjects>>> GetAllExamTypeSubjectsAsync(
         string userId,CancellationToken token,string? filter = null, 
         string? orderBy = "Sequence", int? pageNumber = 1, int? pageSize = 10)
    {
        var query = from examSubjects in _tutorialAppContext
                .ExamSubjects
            join status in _tutorialAppContext.LkpStatuses on examSubjects.StatusId equals status.Id
            join examType in _tutorialAppContext.ExamTypes on examSubjects.ExamTypeId equals examType.Id
            where examSubjects.IsActive
            select new GetAllExamTypeSubjects
            {
                Id = examSubjects.Id,
                ExamType = examType.ExamType1,
                SubjectsName =examSubjects.SubjectName,
                Sequence = examSubjects.Sequence,
                Status = status.StatusName
            };
        
        if (!query.Any())
        {
            return new ResponseViewModelGeneric<List<GetAllExamTypeSubjects>>
            {
                StatusCode = 500,
                Success = false,
                Message = "No Exam Subjects Found!",
            };
        }
        
        // Apply filtering
        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(exam => 
                exam.ExamType.Contains(filter) || // Add more filter conditions as needed
                exam.SubjectsName.Contains(filter) ||
                exam.Status.Contains(filter)
            );
        }
        
        // Apply sorting
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            query = orderBy switch
            {
                "Sequence" => query.OrderByDescending(exam => exam.Sequence),
                _ => query
            };
        }
        
        // Apply pagination
        var totalItems = await query.CountAsync(token);
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize!);
        var pagedData = 
            await query.Skip((int)((pageNumber - 1) * pageSize)!).Take((int)pageSize).ToListAsync(token);

        return new ResponseViewModelGeneric<List<GetAllExamTypeSubjects>>(pagedData)
        {
            StatusCode = 200,
            Success = true,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Message = "Exam Subjects Found!",
        };
    }
}