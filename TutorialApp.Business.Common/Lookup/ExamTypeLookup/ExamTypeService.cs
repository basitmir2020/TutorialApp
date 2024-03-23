using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using TutorialApp.Business.Common.ViewModel;
using TutorialApp.Infrastructure.DB;
using TutorialApp.Infrastructure.Models;

namespace TutorialApp.Business.Common.Lookup.ExamTypeLookup;

public class ExamTypeService : IExamTypeService
{
    private readonly TutorialAppContext _tutorialAppContext;
    public ExamTypeService(TutorialAppContext tutorialAppContext)
    {
        _tutorialAppContext = tutorialAppContext;
    }
    public async Task<ResponseViewModelGeneric<List<ExamTypeDto>>> GetAllExamTypeAsync(CancellationToken token)
    {
        var examTypes = await _tutorialAppContext.LkpExamTypes
            .Where(x => x.IsActive == true && x.Status == "Activated")
            .OrderBy(x => x.Sequence)
            .ToListAsync(token);
        if (examTypes.Count > 0)
        {
            var response = TinyMapper.Map<List<LkpExamTypes>, List<ExamTypeDto>>(examTypes);
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