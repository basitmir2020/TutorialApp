namespace TutorialApp.Business.Common.ViewModel;

public class ResponseViewModelGeneric<T>
{
    public ResponseViewModelGeneric() { }
    public ResponseViewModelGeneric(T data) { Data = data; }
    
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public T? Data { get; set; }
    
}