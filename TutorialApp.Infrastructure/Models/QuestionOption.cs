using System;
using System.Collections.Generic;

namespace TutorialApp.Infrastructure.Models
{
    public partial class QuestionOption
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Options4 { get; set; }
        public bool IsActive { get; set; }
        public int? StatusId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ChapterQuestion? Question { get; set; }
        public virtual LkpStatus? Status { get; set; }
    }
}
