using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Admin.Exams;

public interface IExamService
{
    Task<ResponseViewModel> SaveExamTypeAsync(ExamTypeDto model,string userId,CancellationToken token);
    Task<ResponseViewModelGeneric<List<GetAllExamTypes>>> GetAllExamTypesAsync(string userId,CancellationToken token);
    Task<ResponseViewModelGeneric<GetExamType>> GetExamTypeByIdAsync(string userId,int examTypeId,CancellationToken token);
    Task<ResponseViewModel> UpdateExamType(string userId,GetExamType model,CancellationToken token);
    Task<ResponseViewModel> ChangeStatusAsync(string userId,ChangeStatus model,CancellationToken token);
    Task<ResponseViewModel> DeleteExamTypeAsync(string userId,DeleteExamType model,CancellationToken token);
}