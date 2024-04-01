using System;
using System.Collections.Generic;

namespace TutorialApp.Infrastructure.Models
{
    public partial class ExamType
    {
        public ExamType()
        {
            ExamSubjects = new HashSet<ExamSubject>();
            UserExamTypes = new HashSet<UserExamType>();
        }

        public int Id { get; set; }
        public int? CountryId { get; set; }
        public string ExamType1 { get; set; } = null!;
        public string ExamSubType { get; set; } = null!;
        public int? Sequence { get; set; }
        public bool IsActive { get; set; }
        public int? StatusId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual LkpCountry? Country { get; set; }
        public virtual LkpStatus? Status { get; set; }
        public virtual ICollection<ExamSubject> ExamSubjects { get; set; }
        public virtual ICollection<UserExamType> UserExamTypes { get; set; }
    }
}
