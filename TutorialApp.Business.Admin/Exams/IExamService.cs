using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Admin.Exams;

public interface IExamService
{
    Task<ResponseViewModel> SaveExamTypeAsync(ExamTypeDto model,string userId,CancellationToken token);
    Task<ResponseViewModelGeneric<List<GetAllExamTypes>>> 
        GetAllExamTypesAsync(string userId,CancellationToken token,string? filter = null, string? orderBy = "Sequence", int? pageNumber = 1, int? pageSize = 10);
    Task<ResponseViewModelGeneric<GetExamType>> GetExamTypeByIdAsync(string userId,int examTypeId,CancellationToken token);
    Task<ResponseViewModel> UpdateExamTypeAsync(string userId,GetExamType model,CancellationToken token);
    Task<ResponseViewModel> ChangeStatusAsync(string userId,ChangeStatus model,CancellationToken token);
    Task<ResponseViewModel> DeleteExamTypeAsync(string userId,DeleteExamType model,CancellationToken token);

    Task<ResponseViewModelGeneric<List<ExamTypeVM>>>
        SelectExamTypeAsync(CancellationToken token);
}