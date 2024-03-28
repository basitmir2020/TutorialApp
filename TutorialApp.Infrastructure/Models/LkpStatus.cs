using System;
using System.Collections.Generic;

namespace TutorialApp.Infrastructure.Models
{
    public partial class LkpStatus
    {
        public LkpStatus()
        {
            ChapterQuestions = new HashSet<ChapterQuestion>();
            ExamSubjects = new HashSet<ExamSubject>();
            ExamTypes = new HashSet<ExamType>();
            QuestionOptions = new HashSet<QuestionOption>();
            SubjectChapters = new HashSet<SubjectChapter>();
        }

        public int Id { get; set; }
        public string? StatusName { get; set; }
        public int? Sequence { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<ChapterQuestion> ChapterQuestions { get; set; }
        public virtual ICollection<ExamSubject> ExamSubjects { get; set; }
        public virtual ICollection<ExamType> ExamTypes { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
        public virtual ICollection<SubjectChapter> SubjectChapters { get; set; }
    }
}
