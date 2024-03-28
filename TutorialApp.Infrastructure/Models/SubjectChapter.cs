using System;
using System.Collections.Generic;

namespace TutorialApp.Infrastructure.Models
{
    public partial class SubjectChapter
    {
        public SubjectChapter()
        {
            ChapterQuestions = new HashSet<ChapterQuestion>();
        }

        public int Id { get; set; }
        public int? SubjectId { get; set; }
        public string ChapterName { get; set; } = null!;
        public int? Sequence { get; set; }
        public bool IsActive { get; set; }
        public int? StatusId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual LkpStatus? Status { get; set; }
        public virtual ExamSubject? Subject { get; set; }
        public virtual ICollection<ChapterQuestion> ChapterQuestions { get; set; }
    }
}
