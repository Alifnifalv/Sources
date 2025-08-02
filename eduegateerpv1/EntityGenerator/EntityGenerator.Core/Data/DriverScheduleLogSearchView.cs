using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DriverScheduleLogSearchView
    {
        public long? DriverScheduleLogIID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(555)]
        public string StudentName { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(555)]
        public string EmployeeName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SheduleDate { get; set; }
        [StringLength(250)]
        public string StopEntryStatus { get; set; }
        [StringLength(4000)]
        public string ScheduleLogStatus { get; set; }
        [StringLength(4000)]
        public string ScheduleLogType { get; set; }
        [StringLength(4000)]
        public string Status { get; set; }
        [StringLength(100)]
        public string RouteCode { get; set; }
        [StringLength(50)]
        public string StopName { get; set; }
        [StringLength(50)]
        public string VehicleRegistrationNumber { get; set; }
    }
}
