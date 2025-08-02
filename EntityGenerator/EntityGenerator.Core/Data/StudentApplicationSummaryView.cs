using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentApplicationSummaryView
    {
        public int? TotalApplications { get; set; }
        public int? TotalApplied { get; set; }
        public int? Totalreviewed { get; set; }
        public int? TotalAccepted { get; set; }
        public int? TotalAdmitted { get; set; }
        public int? TotalVarified { get; set; }
        public int? TotalRejected { get; set; }
        public int? TotalPendingforFee { get; set; }
        public byte? SchoolID { get; set; }
    }
}
