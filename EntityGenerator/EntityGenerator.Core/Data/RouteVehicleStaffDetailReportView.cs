using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class RouteVehicleStaffDetailReportView
    {
        public long AssignVehicleMapIID { get; set; }
        [StringLength(100)]
        public string RouteCode { get; set; }
        [StringLength(15)]
        public string ContactNumber { get; set; }
        [StringLength(50)]
        public string VehicleRegistrationNumber { get; set; }
        [StringLength(50)]
        public string StopName { get; set; }
        public long? DriverID { get; set; }
        public int? RouteID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? IsActive { get; set; }
        public long? VehicleID { get; set; }
        [StringLength(50)]
        public string DriverEmployeeCode { get; set; }
        [Required]
        [StringLength(502)]
        public string DriverName { get; set; }
        [StringLength(25)]
        public string DriverContact1 { get; set; }
        [StringLength(50)]
        public string DriverContact2 { get; set; }
        [StringLength(20)]
        public string DriverQID { get; set; }
        [StringLength(20)]
        public string AttendarQID { get; set; }
        public long? AttendantID { get; set; }
        [StringLength(50)]
        public string AttenderEmployeeCode { get; set; }
        [Required]
        [StringLength(502)]
        public string AttenderName { get; set; }
        [StringLength(25)]
        public string AttendarContact1 { get; set; }
        [StringLength(50)]
        public string AttendarContact2 { get; set; }
        public bool? StopActive { get; set; }
        public long? StopID { get; set; }
    }
}
