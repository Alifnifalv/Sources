using Microsoft.EntityFrameworkCore;
using System;

namespace Eduegate.Domain.Entity.Models.Clinic
{
    [Keyless]
    public partial class SL_Department
    {
        public decimal SLDepartmentID { get; set; }
        public string SLDepartmentName { get; set; }
        public Nullable<decimal> SLParentCat { get; set; }
        public string SLPage { get; set; }
        public string SlClinic { get; set; }
        public Nullable<long> SLThumb { get; set; }
        public Nullable<long> SLThumbImage { get; set; }
        public string SLDepartDescription { get; set; }
        public string SLDepartmentNameAr { get; set; }
        public string SLDepartDescriptionAr { get; set; }
        public Nullable<int> SLDord { get; set; }
    }
}
