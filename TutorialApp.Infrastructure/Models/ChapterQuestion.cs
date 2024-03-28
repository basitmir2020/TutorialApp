using System;
using System.Collections.Generic;

namespace TutorialApp.Infrastructure.Models
{
    public partial class ChapterQuestion
    {
        public ChapterQuestion()
        {
            QuestionOptions = new HashSet<QuestionOption>();
        }

        public int Id { get; set; }
        public int? ChapterId { get; set; }
        public string Question { get; set; } = null!;
        public int? CorrectOption { get; set; }
        public int? Sequence { get; set; }
        public bool IsActive { get; set; }
        public int? StatusId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual SubjectChapter? Chapter { get; set; }
        public virtual LkpStatus? Status { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
    }
}
