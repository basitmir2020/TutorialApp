namespace TutorialApp.Business.Common.ViewModel;

public class TokenResponseViewModel
{
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public string Token { get; set; } = null!;
}