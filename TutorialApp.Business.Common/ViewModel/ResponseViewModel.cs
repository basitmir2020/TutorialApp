using System.Text.Json;

namespace TutorialApp.Business.Common.ViewModel;

public class ResponseViewModel
{
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = null!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}