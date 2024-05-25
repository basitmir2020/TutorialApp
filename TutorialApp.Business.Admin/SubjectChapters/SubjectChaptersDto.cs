namespace TutorialApp.Business.Admin.SubjectChapters;

public class SubjectChaptersDto
{
    public int SubjectId { get; set; }
    public string ChapterName { get; set; } = null!;
}

public class GetAllSubjectChapters
{
    public int Id { get; set; }
    public int? Sequence { get; set; }
    public string SubjectName { get; set; } = null!;
    public string ChapterName { get; set; } = null!;
    public string Status { get; set; } = null!;
}

public class GetSubjectChapters
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public string ChapterName { get; set; } = null!;
}

public class SubjectChapterStatus
{
    public int SubjectChapterId { get; set; }
    public int StatusId { get; set; }
}

public class DeleteSubjectChapters
{
    public int SubjectChapterId { get; set; }
}

public class SubjectChaptersVM
{
    public int Id { get; set; }
    public string ChapterName { get; set; } = null!;
    public int Sequence { get; set; }
}

