using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmployeeTimeSheetsSearchView
    {
        public long? EmployeeTimeSheetIID { get; set; }
        public long EmployeeID { get; set; }
        [StringLength(502)]
        public string Employee { get; set; }
        [Column(TypeName = "date")]
        public DateTime TimesheetDate { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? NormalHours { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? OTHours { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? TotalHours { get; set; }
    }
}
