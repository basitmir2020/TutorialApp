using System;
using System.Collections.Generic;
using TutorialApp.Infrastructure.Identity;

namespace TutorialApp.Infrastructure.Models
{
    public partial class UserExamType
    {
        public int Id { get; set; }
        public int? ExamTypeId { get; set; }
        public string? UserId { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ExamType? ExamType { get; set; }
        public virtual AspNetUser? User { get; set; }
    }
}
