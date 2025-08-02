using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentRouteStopMaps", Schema = "schools")]
    [Index("AcademicYearID", "DropStopRouteID", Name = "IDX_StudentRouteStopMaps_AcademicYearIDDropStopRouteID_IsActive")]
    [Index("AcademicYearID", "PickupRouteID", Name = "IDX_StudentRouteStopMaps_AcademicYearIDPickupRouteID_IsActive")]
    [Index("AcademicYearID", "SchoolID", Name = "IDX_StudentRouteStopMaps_AcademicYearID__SchoolID_")]
    [Index("AcademicYearID", "SchoolID", Name = "IDX_StudentRouteStopMaps_AcademicYearID__SchoolID_IsActive")]
    [Index("AcademicYearID", "SchoolID", Name = "IDX_StudentRouteStopMaps_AcademicYearID__SchoolID_IsActive__PickupRouteID")]
    [Index("AcademicYearID", "SchoolID", Name = "IDX_StudentRouteStopMaps_AcademicYearID__SchoolID_StudentID__PickupStopMapID__DropStopMapID__IsOneW")]
    [Index("DropStopMapID", "IsActive", "AcademicYearID", Name = "IDX_StudentRouteStopMaps_DropStopMapID__IsActive__AcademicYearID_")]
    [Index("IsActive", "AcademicYearID", "SchoolID", "SectionID", "ClassID", Name = "IDX_StudentRouteStopMaps_IsActive_AcademicYearID_SchoolID_SectionID_ClassID")]
    [Index("IsActive", Name = "IDX_StudentRouteStopMaps_IsActive_DropStopRouteID__AcademicYearID")]
    [Index("IsActive", Name = "IDX_StudentRouteStopMaps_IsActive_PickupRouteID__AcademicYearID")]
    [Index("IsActive", "AcademicYearID", "SchoolID", Name = "IDX_StudentRouteStopMaps_IsActive__AcademicYearID__SchoolID_StudentID__PickupStopMapID__DropStopMap")]
    [Index("IsActive", "DropStopRouteID", "AcademicYearID", Name = "IDX_StudentRouteStopMaps_IsActive__DropStopRouteID__AcademicYearID_")]
    [Index("IsActive", "PickupRouteID", "AcademicYearID", Name = "IDX_StudentRouteStopMaps_IsActive__PickupRouteID__AcademicYearID_")]
    [Index("PickupRouteID", "AcademicYearID", "SchoolID", Name = "IDX_StudentRouteStopMaps_PickupRouteID__AcademicYearID__SchoolID_IsActive")]
    [Index("PickupRouteID", "AcademicYearID", "SchoolID", Name = "IDX_StudentRouteStopMaps_PickupRouteID__AcademicYearID__SchoolID_StudentID__PickupStopMapID__DropSt")]
    [Index("PickupStopMapID", "IsActive", "AcademicYearID", Name = "IDX_StudentRouteStopMaps_PickupStopMapID__IsActive__AcademicYearID_")]
    [Index("SchoolID", Name = "IDX_StudentRouteStopMaps_SchoolID_IsActive")]
    [Index("StudentID", "IsActive", Name = "IDX_StudentRouteStopMaps_StudentID_IsActive")]
    [Index("IsActive", Name = "StudentRouteStopMaps_IsActive")]
    [Index("StudentID", "IsActive", "StudentRouteStopMapIID", "PickupStopMapID", "DropStopRouteID", "PickupRouteID", "DropStopMapID", "DateFrom", "DateTo", "IsOneWay", Name = "_dta_index_StudentRouteStopMaps_7_1034538819__K2_K9_K1_K4_K16_K15_K5_K7_K8_K6")]
    public partial class StudentRouteStopMap
    {
        public StudentRouteStopMap()
        {
            EventTransportAllocationMaps = new HashSet<EventTransportAllocationMap>();
            StudentRouteMonthlySplits = new HashSet<StudentRouteMonthlySplit>();
            StudentRoutePeriodMaps = new HashSet<StudentRoutePeriodMap>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
        }

        [Key]
        public long StudentRouteStopMapIID { get; set; }
        public long? StudentID { get; set; }
        public long? RouteStopMapID { get; set; }
        public long? PickupStopMapID { get; set; }
        public long? DropStopMapID { get; set; }
        public bool? IsOneWay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? PickupRouteID { get; set; }
        public int? DropStopRouteID { get; set; }
        public bool? Termsandco { get; set; }
        [StringLength(20)]
        public string StudentRouteStopCode { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public long? TransportStatusID { get; set; }
        public string Remarks { get; set; }
        public int? SectionID { get; set; }
        public int? ClassID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PickupTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DropTime { get; set; }
        public int? IsRouteShifted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelDate { get; set; }
        public int? FeePeriodID { get; set; }
        public long? TransportApplctnStudentMapID { get; set; }
        public bool? IsFromPromotion { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentRouteStopMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("StudentRouteStopMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("DropStopMapID")]
        [InverseProperty("StudentRouteStopMapDropStopMaps")]
        public virtual RouteStopMap DropStopMap { get; set; }
        [ForeignKey("DropStopRouteID")]
        [InverseProperty("StudentRouteStopMapDropStopRoutes")]
        public virtual Route1 DropStopRoute { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("StudentRouteStopMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("PickupRouteID")]
        [InverseProperty("StudentRouteStopMapPickupRoutes")]
        public virtual Route1 PickupRoute { get; set; }
        [ForeignKey("PickupStopMapID")]
        [InverseProperty("StudentRouteStopMapPickupStopMaps")]
        public virtual RouteStopMap PickupStopMap { get; set; }
        [ForeignKey("RouteStopMapID")]
        [InverseProperty("StudentRouteStopMapRouteStopMaps")]
        public virtual RouteStopMap RouteStopMap { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentRouteStopMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("StudentRouteStopMaps")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentRouteStopMaps")]
        public virtual Student Student { get; set; }
        [ForeignKey("TransportStatusID")]
        [InverseProperty("StudentRouteStopMaps")]
        public virtual TransportStatus TransportStatus { get; set; }
        [InverseProperty("StudentRouteStopMap")]
        public virtual ICollection<EventTransportAllocationMap> EventTransportAllocationMaps { get; set; }
        [InverseProperty("StudentRouteStopMap")]
        public virtual ICollection<StudentRouteMonthlySplit> StudentRouteMonthlySplits { get; set; }
        [InverseProperty("StudentRouteStopMap")]
        public virtual ICollection<StudentRoutePeriodMap> StudentRoutePeriodMaps { get; set; }
        [InverseProperty("StudentRouteStopMap")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }
    }
}
