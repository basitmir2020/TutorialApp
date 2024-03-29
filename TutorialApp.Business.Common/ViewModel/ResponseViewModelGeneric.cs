namespace TutorialApp.Business.Common.ViewModel;

public class ResponseViewModelGeneric<T>
{
    public ResponseViewModelGeneric()
    {
    }

    public ResponseViewModelGeneric(T data)
    {
        Data = data;
    }

    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public int? TotalItems { get; set; }
    public int? TotalPages { get; set; }
    public T? Data { get; set; }
}