namespace TutorialApp.Infrastructure.Models
{
    public partial class ExamSubject
    {
        public ExamSubject()
        {
            SubjectChapters = new HashSet<SubjectChapter>();
        }

        public int Id { get; set; }
        public int? ExamTypeId { get; set; }
        public string SubjectName { get; set; } = null!;
        public int? Sequence { get; set; }
        public bool IsActive { get; set; }
        public int? StatusId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ExamType? ExamType { get; set; }
        public virtual LkpStatus? Status { get; set; }
        public virtual ICollection<SubjectChapter> SubjectChapters { get; set; }
    }
}
