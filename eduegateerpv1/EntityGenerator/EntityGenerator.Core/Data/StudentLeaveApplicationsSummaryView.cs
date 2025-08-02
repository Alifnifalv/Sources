using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentLeaveApplicationsSummaryView
    {
        public int? TotalApplication { get; set; }
        public int? TotalSubmitted { get; set; }
        public int? TotalApproved { get; set; }
        public int? TotalRejected { get; set; }
        public int? TodaysRequest { get; set; }
    }
}
