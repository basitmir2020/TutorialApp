using Microsoft.EntityFrameworkCore;
using TutorialApp.Business.Admin.Exams;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Models;

namespace TutorialApp.Business.Admin.SubjectChapters;

public class SubjectChaptersService : ISubjectChapters
{
    private readonly TutorialAppContext _tutorialAppContext;

    public SubjectChaptersService(TutorialAppContext tutorialAppContext)
    {
        _tutorialAppContext = tutorialAppContext;
    }
    public async Task<ResponseViewModel> SaveSubjectChaptersAsync(SubjectChaptersDto model, string userId, CancellationToken token)
    {
        var examTypes = await _tutorialAppContext.SubjectChapters
            .FirstOrDefaultAsync(x => x.SubjectId == model.SubjectId &&
                                      x.ChapterName == model.ChapterName &&
                                      x.IsActive == true, cancellationToken: token);
        if (examTypes != null)
        {
            return new ResponseViewModel
            {
                StatusCode = 400,
                Message = "Subject Chapter Already Created!",
                Success = false
            };
        }

        var subjectChapters = await _tutorialAppContext
            .SubjectChapters
            .ToListAsync(cancellationToken: token);
        var newExamTypes = new SubjectChapter
        {
            SubjectId = model.SubjectId,
            ChapterName = model.ChapterName,
            IsActive = true,
            StatusId = 1,
            Sequence = subjectChapters.Count + 1,
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
            Message = "Subject Chapters Saved Successfully!",
            Success = true
        };
    }

    public async Task<ResponseViewModelGeneric<List<GetAllSubjectChapters>>> 
        GetAllSubjectChaptersAsync(string userId, CancellationToken token, string? filter = null,
        string? orderBy = "Sequence", int? pageNumber = 1, int? pageSize = 10)
    {
        var query = from subjectChapters in _tutorialAppContext.SubjectChapters
            join status in _tutorialAppContext.LkpStatuses on subjectChapters.StatusId equals status.Id
            join examSubject in _tutorialAppContext.ExamSubjects on subjectChapters.SubjectId equals examSubject.Id
            where subjectChapters.IsActive
            select new GetAllSubjectChapters
            {
                Id = subjectChapters.Id,
                Sequence = subjectChapters.Sequence,
                SubjectName = examSubject.SubjectName + " (" + _tutorialAppContext.ExamTypes.Where(x=>x.Id == examSubject.ExamTypeId).Select(x=>x.ExamType1).FirstOrDefault() +")",
                ChapterName = subjectChapters.ChapterName,
                Status = status.StatusName
            };
        
        if (!query.Any())
        {
            return new ResponseViewModelGeneric<List<GetAllSubjectChapters>>
            {
                StatusCode = 500,
                Success = false,
                Message = "No Subject Chapters Found!",
            };
        }
        
        // Apply filtering
        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(exam => 
                exam.SubjectName.Contains(filter) || // Add more filter conditions as needed
                exam.ChapterName.Contains(filter) ||
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
        var pagedData = await query.Skip((int)((pageNumber - 1) * pageSize)!).Take((int)pageSize).ToListAsync(token);

        return new ResponseViewModelGeneric<List<GetAllSubjectChapters>>(pagedData)
        {
            StatusCode = 200,
            Success = true,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Message = "Subject Chapters Found!",
        };
    }

    public async Task<ResponseViewModelGeneric<GetSubjectChapters>> GetSubjectChaptersByIdAsync(string userId, int subjectChapterId, CancellationToken token)
    {
        var existSubjectChapter = await _tutorialAppContext.SubjectChapters
            .FirstOrDefaultAsync(x => x.Id == subjectChapterId,token);
        if (existSubjectChapter == null)
        {
            return new ResponseViewModelGeneric<GetSubjectChapters>
            {
                StatusCode = 500,
                Success = false,
                Message = "Subject Chapter Not Found!",
            };
        }

        var response = new GetSubjectChapters
        {
            Id = existSubjectChapter.Id,
            SubjectId = Convert.ToInt32(existSubjectChapter.SubjectId),
            ChapterName = existSubjectChapter.ChapterName
        };

        return new ResponseViewModelGeneric<GetSubjectChapters>(response)
        {
            StatusCode = 200,
            Success = true,
            Message = "Subject Chapters Data!"
        };
    }

    public async Task<ResponseViewModel> UpdateSubjectChaptersAsync(string userId, GetSubjectChapters model, CancellationToken token)
    {
        var existSubjectChapter = await _tutorialAppContext.SubjectChapters
            .FirstOrDefaultAsync(x => x.Id == model.Id,token);
        if (existSubjectChapter == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 500,
                Success = false,
                Message = "Subject Chapter Not Found"
            };
        }

        existSubjectChapter.SubjectId = model.SubjectId;
        existSubjectChapter.ChapterName = model.ChapterName;
        existSubjectChapter.ModifiedOn = DateTime.Now;
        existSubjectChapter.ModifiedBy = userId;
        _tutorialAppContext.Update(existSubjectChapter);
        await _tutorialAppContext.SaveChangesAsync(token);

        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Subject Chapter Updated Successfully!"
        };
    }

    public async Task<ResponseViewModel> ChangeSubjectChaptersStatusAsync(string userId, SubjectChapterStatus model, CancellationToken token)
    {
        var existSubjectChapter = await _tutorialAppContext.SubjectChapters
            .FirstOrDefaultAsync(x => x.Id == model.SubjectChapterId,token);
        if (existSubjectChapter == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 500,
                Success = false,
                Message = "Subject Chapter Not Found!",
            };
        }

        existSubjectChapter.StatusId = model.StatusId;
        existSubjectChapter.ModifiedOn = DateTime.Now;
        existSubjectChapter.ModifiedBy = userId;
        _tutorialAppContext.Update(existSubjectChapter);
        await _tutorialAppContext.SaveChangesAsync(token);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Status Changed Successfully!",
        };
    }

    public async Task<ResponseViewModel> DeleteSubjectChaptersAsync(string userId, DeleteSubjectChapters model, CancellationToken token)
    {
        var existExamType = await _tutorialAppContext.SubjectChapters
            .FirstOrDefaultAsync(x => x.Id == model.SubjectChapterId,token);
        if (existExamType == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 500,
                Success = false,
                Message = "Subject Chapter Not Found!",
            };
        }

        existExamType.IsActive = false;
        existExamType.ModifiedOn = DateTime.Now;
        existExamType.ModifiedBy = userId;
        _tutorialAppContext.Update(existExamType);
        await _tutorialAppContext.SaveChangesAsync(token);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Subject Chapter deleted successfully!",
        };
    }

    public async Task<ResponseViewModelGeneric<List<SubjectChaptersVM>>> SelectSubjectChaptersAsync(CancellationToken token)
    {
        var subjectList = await (from subjectChapters in _tutorialAppContext.SubjectChapters
            where subjectChapters.IsActive
            select new SubjectChaptersVM
            {
                Id = subjectChapters.Id,
                ChapterName = subjectChapters.ChapterName,
                Sequence = Convert.ToInt32(subjectChapters.Sequence)
            }).OrderByDescending(x=>x.Sequence)
            .ToListAsync(cancellationToken: token);
        return new ResponseViewModelGeneric<List<SubjectChaptersVM>>(subjectList)
        {
            Success = true,
            StatusCode = 200,
            Message = "Select Subject Chapters"
        };
    }
}