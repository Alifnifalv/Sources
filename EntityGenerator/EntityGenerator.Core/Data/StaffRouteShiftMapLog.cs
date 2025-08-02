using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StaffRouteShiftMapLogs", Schema = "schools")]
    public partial class StaffRouteShiftMapLog
    {
        [Key]
        public long StaffRouteStopMapLogIID { get; set; }
        public long? StaffRouteStopMapID { get; set; }
        public long? StaffID { get; set; }
        public long? PickupStopMapID { get; set; }
        public long? DropStopMapID { get; set; }
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
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public long? TransportStatusID { get; set; }
        public string Remarks { get; set; }
        public int? IsRouteShifted { get; set; }
        public int? ShiftFromRouteID { get; set; }
        [StringLength(500)]
        public string OldPickUpStop { get; set; }
        [StringLength(500)]
        public string OldDropStop { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StaffRouteShiftMapLogs")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("DropStopMapID")]
        [InverseProperty("StaffRouteShiftMapLogDropStopMaps")]
        public virtual RouteStopMap DropStopMap { get; set; }
        [ForeignKey("PickupStopMapID")]
        [InverseProperty("StaffRouteShiftMapLogPickupStopMaps")]
        public virtual RouteStopMap PickupStopMap { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StaffRouteShiftMapLogs")]
        public virtual School School { get; set; }
        [ForeignKey("ShiftFromRouteID")]
        [InverseProperty("StaffRouteShiftMapLogs")]
        public virtual Route1 ShiftFromRoute { get; set; }
        [ForeignKey("StaffID")]
        [InverseProperty("StaffRouteShiftMapLogs")]
        public virtual Employee Staff { get; set; }
        [ForeignKey("StaffRouteStopMapID")]
        [InverseProperty("StaffRouteShiftMapLogs")]
        public virtual StaffRouteStopMap StaffRouteStopMap { get; set; }
        [ForeignKey("TransportStatusID")]
        [InverseProperty("StaffRouteShiftMapLogs")]
        public virtual TransportStatus TransportStatus { get; set; }
    }
}
