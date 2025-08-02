using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentRouteStopMapLogs", Schema = "schools")]
    public partial class StudentRouteStopMapLog
    {
        [Key]
        public long StudentRouteStopMapLogIID { get; set; }
        public long? StudentRouteStopMapID { get; set; }
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
        public int? RouteID { get; set; }
        [StringLength(500)]
        public string OldPickUpStop { get; set; }
        [StringLength(500)]
        public string OldDropStop { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentRouteStopMapLogs")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("StudentRouteStopMapLogs")]
        public virtual Class Class { get; set; }
        [ForeignKey("DropStopMapID")]
        [InverseProperty("StudentRouteStopMapLogDropStopMaps")]
        public virtual RouteStopMap DropStopMap { get; set; }
        [ForeignKey("DropStopRouteID")]
        [InverseProperty("StudentRouteStopMapLogDropStopRoutes")]
        public virtual Route1 DropStopRoute { get; set; }
        [ForeignKey("PickupRouteID")]
        [InverseProperty("StudentRouteStopMapLogPickupRoutes")]
        public virtual Route1 PickupRoute { get; set; }
        [ForeignKey("PickupStopMapID")]
        [InverseProperty("StudentRouteStopMapLogPickupStopMaps")]
        public virtual RouteStopMap PickupStopMap { get; set; }
        [ForeignKey("RouteID")]
        [InverseProperty("StudentRouteStopMapLogRoutes")]
        public virtual Route1 Route { get; set; }
        [ForeignKey("RouteStopMapID")]
        [InverseProperty("StudentRouteStopMapLogRouteStopMaps")]
        public virtual RouteStopMap RouteStopMap { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentRouteStopMapLogs")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("StudentRouteStopMapLogs")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentRouteStopMapLogs")]
        public virtual Student Student { get; set; }
        [ForeignKey("StudentRouteStopMapID")]
        [InverseProperty("StudentRouteStopMapLogs")]
        public virtual StudentRouteStopMap StudentRouteStopMap { get; set; }
        [ForeignKey("TransportStatusID")]
        [InverseProperty("StudentRouteStopMapLogs")]
        public virtual TransportStatus TransportStatus { get; set; }
    }
}
