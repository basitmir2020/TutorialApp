using System;
using System.Collections.Generic;

namespace TutorialApp.Infrastructure.Models
{
    public partial class LkpCountry
    {
        public LkpCountry()
        {
            ExamTypes = new HashSet<ExamType>();
        }
        
        public int Id { get; set; }
        public string? CountryName { get; set; }
        public string? CountryInitial { get; set; }
        public string? CountryCode { get; set; }
        public bool IsActive { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? Sequence { get; set; }
        
        public virtual ICollection<ExamType> ExamTypes { get; set; }
    }
}
