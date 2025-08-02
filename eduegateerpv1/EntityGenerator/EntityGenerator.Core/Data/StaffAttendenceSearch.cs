using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StaffAttendenceSearch
    {
        public long StaffAttendenceIID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? AttendenceDate { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(502)]
        public string EmployeeName { get; set; }
        public byte? PresentStatusID { get; set; }
        [StringLength(50)]
        public string StatusDescription { get; set; }
        public long? SchoolID { get; set; }
        public long BranchIID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
