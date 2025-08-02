using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DriverScheduleLogs", Schema = "schools")]
    [Index("EmployeeID", "RouteID", "VehicleID", "ScheduleLogType", "StudentID", Name = "IDX_DriverScheduleLogs_EmployeeID__RouteID__VehicleID__ScheduleLogTypeStudentID_SheduleDate__RouteS")]
    [Index("EmployeeID", "RouteID", "VehicleID", "Status", "ScheduleLogType", "StudentID", Name = "IDX_DriverScheduleLogs_EmployeeID__RouteID__VehicleID__Status__ScheduleLogTypeStudentID_SheduleDate")]
    [Index("RouteID", "SheduleDate", Name = "IDX_DriverScheduleLogs_RouteIDSheduleDate_")]
    [Index("RouteID", "SheduleDate", Name = "IDX_DriverScheduleLogs_RouteIDSheduleDate_StudentID__EmployeeID__RouteStopMapID__VehicleID__Shedule")]
    [Index("RouteID", "RouteStopMapID", "VehicleID", Name = "IDX_DriverScheduleLogs_RouteID__RouteStopMapID__VehicleID_StudentID__EmployeeID__SheduleDate__Shedu")]
    [Index("RouteID", "ScheduleLogType", "SheduleDate", Name = "IDX_DriverScheduleLogs_RouteID__ScheduleLogTypeSheduleDate_")]
    [Index("RouteID", "ScheduleLogType", "SheduleDate", Name = "IDX_DriverScheduleLogs_RouteID__ScheduleLogTypeSheduleDate_StudentID__EmployeeID__RouteStopMapID__V")]
    [Index("RouteID", "Status", "ScheduleLogType", "SheduleDate", Name = "IDX_DriverScheduleLogs_RouteID__Status__ScheduleLogTypeSheduleDate_")]
    [Index("RouteID", "VehicleID", Name = "IDX_DriverScheduleLogs_RouteID__VehicleID_")]
    [Index("RouteID", "VehicleID", Name = "IDX_DriverScheduleLogs_RouteID__VehicleID_StudentID__EmployeeID__SheduleDate__RouteStopMapID__Shedu")]
    [Index("RouteID", "VehicleID", "Status", "SheduleDate", "ScheduleLogType", Name = "IDX_DriverScheduleLogs_RouteID__VehicleID__StatusSheduleDate__ScheduleLogType_StudentID__EmployeeID")]
    [Index("ScheduleLogType", Name = "IDX_DriverScheduleLogs_ScheduleLogType_StudentID__EmployeeID__SheduleDate__RouteID__RouteStopMapID_")]
    [Index("SheduleDate", Name = "IDX_DriverScheduleLogs_SheduleDate_")]
    [Index("SheduleDate", Name = "IDX_DriverScheduleLogs_SheduleDate_StudentID__EmployeeID__RouteID__RouteStopMapID__VehicleID__Shedu")]
    [Index("SheduleDate", "RouteID", Name = "IDX_DriverScheduleLogs_SheduleDate__RouteID_")]
    [Index("SheduleDate", "RouteID", "VehicleID", Name = "IDX_DriverScheduleLogs_SheduleDate__RouteID__VehicleID_")]
    [Index("SheduleDate", "ScheduleLogType", Name = "IDX_DriverScheduleLogs_SheduleDate__ScheduleLogType_")]
    [Index("StudentID", "RouteID", "VehicleID", "ScheduleLogType", "EmployeeID", Name = "IDX_DriverScheduleLogs_StudentID__RouteID__VehicleID__ScheduleLogTypeEmployeeID_SheduleDate__RouteS")]
    [Index("StudentID", "RouteID", "VehicleID", "Status", "ScheduleLogType", "EmployeeID", Name = "IDX_DriverScheduleLogs_StudentID__RouteID__VehicleID__Status__ScheduleLogTypeEmployeeID_SheduleDate")]
    [Index("StudentID", "ScheduleLogType", Name = "IDX_DriverScheduleLogs_StudentID__ScheduleLogType_EmployeeID__SheduleDate__RouteID__RouteStopMapID_")]
    [Index("StopEntryStatusID", "RouteID", "RouteStopMapID", "VehicleID", "StudentID", "SheduleDate", "SheduleLogStatusID", Name = "_dta_index_DriverScheduleLogs_7_1252407731__K9_K5_K6_K7_K2_K4_K8_1_3_10_11")]
    [Index("EmployeeID", "RouteID", "RouteStopMapID", "VehicleID", "ScheduleLogType", "StudentID", Name = "dx_DriverScheduleLogsEmployeeIDRouteIDRtStopMapStudentID")]
    public partial class DriverScheduleLog
    {
        [Key]
        public long DriverScheduleLogIID { get; set; }
        public long? StudentID { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SheduleDate { get; set; }
        public int? RouteID { get; set; }
        public long? RouteStopMapID { get; set; }
        public long? VehicleID { get; set; }
        public int? SheduleLogStatusID { get; set; }
        public int? StopEntryStatusID { get; set; }
        [StringLength(10)]
        public string Status { get; set; }
        [StringLength(10)]
        public string ScheduleLogType { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("DriverScheduleLogs")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("RouteID")]
        [InverseProperty("DriverScheduleLogs")]
        public virtual Route1 Route { get; set; }
        [ForeignKey("RouteStopMapID")]
        [InverseProperty("DriverScheduleLogs")]
        public virtual RouteStopMap RouteStopMap { get; set; }
        [ForeignKey("SheduleLogStatusID")]
        [InverseProperty("DriverScheduleLogs")]
        public virtual ScheduleLogStatus SheduleLogStatus { get; set; }
        [ForeignKey("StopEntryStatusID")]
        [InverseProperty("DriverScheduleLogs")]
        public virtual StopEntryStatus StopEntryStatus { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("DriverScheduleLogs")]
        public virtual Student Student { get; set; }
        [ForeignKey("VehicleID")]
        [InverseProperty("DriverScheduleLogs")]
        public virtual Vehicle Vehicle { get; set; }
    }
}
