using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DriverScheduleLogListView
    {
        public long? IID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SheduleDate { get; set; }
        public long? StudentID { get; set; }
        public int? RouteID { get; set; }
        public long? VehicleID { get; set; }
        public long? EmployeeID { get; set; }
        public int? StopEntryStatusID { get; set; }
        [StringLength(10)]
        public string INStatus { get; set; }
        [StringLength(10)]
        public string OUTStatus { get; set; }
        public long? INScheduleIID { get; set; }
        public long? OUTScheduleIID { get; set; }
        [StringLength(4000)]
        public string InTime { get; set; }
        [StringLength(4000)]
        public string OutTime { get; set; }
        [StringLength(50)]
        public string StopName { get; set; }
        [StringLength(100)]
        public string Route { get; set; }
        [StringLength(50)]
        public string Vehicle { get; set; }
        [StringLength(250)]
        public string StatusNameEn { get; set; }
        [StringLength(555)]
        public string StudStaff { get; set; }
        [StringLength(4000)]
        public string ClassSection { get; set; }
        [Required]
        [StringLength(10)]
        public string Attendance { get; set; }
    }
}
