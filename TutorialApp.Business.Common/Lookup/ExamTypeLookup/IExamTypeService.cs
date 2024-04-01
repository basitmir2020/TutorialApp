using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Common.Lookup.ExamTypeLookup;

public interface IExamTypeService
{
    Task<ResponseViewModelGeneric<List<ExamTypeDto>>> GetAllExamTypeAsync(CancellationToken token,string userId);
}