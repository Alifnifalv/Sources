using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.HR.Models
{
    public partial class DB_ApplicationForm
    {
        public long ApplicationFormIID { get; set; }
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string CountryOfResidence { get; set; }
        public string YearsOfExperience { get; set; }
        public string PositionAppliedFor { get; set; }
        public string CV { get; set; }
        public string Nationality { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Qualification { get; set; }
        public string Status { get; set; }
    }
}
