using System;
using System.Collections.Generic;

namespace TutorialApp.Infrastructure.Model
{
    public partial class LkpCountry
    {
        public int Id { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
        public bool IsActive { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? Sequence { get; set; }
    }
}
