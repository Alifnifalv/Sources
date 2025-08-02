using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models.Clinic
{
    [Keyless]
    public partial class SLDoctor
    {
        public decimal SLDoctorID { get; set; }
        public Nullable<decimal> SLMainDepartment { get; set; }
        public Nullable<decimal> SLSubDepartment { get; set; }
        public string SLDoctor1 { get; set; }
        public string SLDoctorAr { get; set; }
        public string SLTiming { get; set; }
        public string SLContact { get; set; }
        public string SLMainDepartmentName { get; set; }
        public string SLSubDepartmentName { get; set; }
        public Nullable<long> SLThumb { get; set; }
        public string SLDesc { get; set; }
        public Nullable<bool> FeatureDoctor { get; set; }
        public string SLVideos { get; set; }
        public string SLWebsite { get; set; }
        public string SLFacebook { get; set; }
        public string SLTwitter { get; set; }
        public string SLFiles { get; set; }
        public string SLPosition { get; set; }
        public string SLDept { get; set; }
        public string SLQualification { get; set; }
        public string SLCertificates { get; set; }
        public string SlClinic { get; set; }
        public string SLPositionAr { get; set; }
        public string SLCertificatesAr { get; set; }
        public Nullable<int> SLDocord { get; set; }
        public Nullable<int> SlCheck { get; set; }
    }
}
