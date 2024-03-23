namespace TutorialApp.Business.Common.Lookup.ExamTypeLookup;

public class ExamTypeDto
{
    public int Id { get; set; }
    public string ExamType { get; set; } = null!;
    public string? ExamTypeIcon { get; set; }
}