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

    public async Task<ResponseViewModel> SaveExamTypeAsync(ExamTypeDto model, string userId, CancellationToken token)
    {
        var examTypes = await _tutorialAppContext.ExamTypes
            .FirstOrDefaultAsync(x => x.ExamType1 == model.ExamType &&
                                      x.CountryId == model.CountryId &&
                                      x.IsActive == true, cancellationToken: token);
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
            .ExamTypes
            .ToListAsync(cancellationToken: token);
        var newExamTypes = new ExamType
        {
            CountryId = model.CountryId,
            ExamType1 = model.ExamType,
            ExamSubType = model.ExamSubType,
            IsActive = true,
            StatusId = 1,
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

    public async Task<ResponseViewModelGeneric<List<GetAllExamTypes>>>
        GetAllExamTypesAsync(string userId,CancellationToken token,string? filter = null,
            string? orderBy = "Sequence", int? pageNumber = 1, int? pageSize = 10)
    {
        var query = from examType in _tutorialAppContext.ExamTypes
            join status in _tutorialAppContext.LkpStatuses on examType.StatusId equals status.Id
            join country in _tutorialAppContext.LkpCountries on examType.CountryId equals country.Id
            where examType.IsActive
            select new GetAllExamTypes
            {
                Id = examType.Id,
                Sequence = examType.Sequence,
                CountryName = country.CountryName,
                ExamType = examType.ExamType1,
                ExamSubType = examType.ExamSubType,
                Status = status.StatusName
            };
        
        if (!query.Any())
        {
            return new ResponseViewModelGeneric<List<GetAllExamTypes>>
            {
                StatusCode = 500,
                Success = false,
                Message = "No Exam Types Found!",
            };
        }
        
        // Apply filtering
        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(exam => 
                exam.ExamType.Contains(filter) || // Add more filter conditions as needed
                exam.ExamSubType.Contains(filter) ||
                exam.CountryName.Contains(filter) ||
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

        return new ResponseViewModelGeneric<List<GetAllExamTypes>>(pagedData)
        {
            StatusCode = 200,
            Success = true,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Message = "Exam Types Found!",
        };
    }

    public async Task<ResponseViewModelGeneric<GetExamType>> GetExamTypeByIdAsync(string userId, int examTypeId, CancellationToken token)
    {
        var existExamType = await _tutorialAppContext.ExamTypes
            .FirstOrDefaultAsync(x => x.Id == examTypeId,token);
        if (existExamType == null)
        {
            return new ResponseViewModelGeneric<GetExamType>
            {
                StatusCode = 500,
                Success = false,
                Message = "Exam Type Not Found!",
            };
        }

        var response = new GetExamType
        {
            Id = existExamType.Id,
            CountryId = existExamType.CountryId,
            ExamType = existExamType.ExamType1,
            ExamSubType = existExamType.ExamSubType
        };

        return new ResponseViewModelGeneric<GetExamType>(response)
        {
            StatusCode = 200,
            Success = true,
            Message = "Exam Type Data!"
        };
    }

    public async Task<ResponseViewModel> UpdateExamTypeAsync(string userId, GetExamType model, CancellationToken token)
    {
        var existExamType = await _tutorialAppContext.ExamTypes
            .FirstOrDefaultAsync(x => x.Id == model.Id,token);
        if (existExamType == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 500,
                Success = false,
                Message = "Exam Type Not Found",
            };
        }

        existExamType.CountryId = model.CountryId;
        existExamType.ExamType1 = model.ExamType;
        existExamType.ExamSubType = model.ExamSubType;
        existExamType.ModifiedOn = DateTime.Now;
        existExamType.ModifiedBy = userId;
        _tutorialAppContext.Update(existExamType);
        await _tutorialAppContext.SaveChangesAsync(token);

        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Exam Type Updated Successfully!"
        };
    }

    public async Task<ResponseViewModel> ChangeStatusAsync(string userId, ChangeStatus model, CancellationToken token)
    {
        var existExamType = await _tutorialAppContext.ExamTypes
            .FirstOrDefaultAsync(x => x.Id == model.ExamTypeId,token);
        if (existExamType == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 500,
                Success = false,
                Message = "Exam Type Not Found!",
            };
        }

        existExamType.StatusId = model.StatusId;
        existExamType.ModifiedOn = DateTime.Now;
        existExamType.ModifiedBy = userId;
        _tutorialAppContext.Update(existExamType);
        await _tutorialAppContext.SaveChangesAsync(token);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Status Changed Successfully!",
        };
    }

    public async Task<ResponseViewModel> DeleteExamTypeAsync(string userId, DeleteExamType model, CancellationToken token)
    {
        var existExamType = await _tutorialAppContext.ExamTypes
            .FirstOrDefaultAsync(x => x.Id == model.ExamTypeId,token);
        if (existExamType == null)
        {
            return new ResponseViewModel
            {
                StatusCode = 500,
                Success = false,
                Message = "Exam Type Not Found!",
            };
        }

        existExamType.IsActive =false;
        existExamType.ModifiedOn = DateTime.Now;
        existExamType.ModifiedBy = userId;
        _tutorialAppContext.Update(existExamType);
        await _tutorialAppContext.SaveChangesAsync(token);
        return new ResponseViewModel
        {
            StatusCode = 200,
            Success = true,
            Message = "Exam Type deleted successfully!",
        };
    }

    public async Task<ResponseViewModelGeneric<List<ExamTypeVM>>> SelectExamTypeAsync(CancellationToken token)
    {
        var examTypes = await (from examType in _tutorialAppContext.ExamTypes
            where examType.IsActive
            select new ExamTypeVM
            {
                Id = examType.Id,
                ExamType = examType.ExamType1
            }).ToListAsync(cancellationToken: token);
        return new ResponseViewModelGeneric<List<ExamTypeVM>>(examTypes)
        {
            Success = true,
            StatusCode = 200,
            Message = "Select Exam Types"
        };
    }
}