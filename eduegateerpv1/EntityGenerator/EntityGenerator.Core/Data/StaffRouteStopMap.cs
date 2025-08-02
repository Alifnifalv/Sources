using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StaffRouteStopMaps", Schema = "schools")]
    public partial class StaffRouteStopMap
    {
        public StaffRouteStopMap()
        {
            EventTransportAllocationMaps = new HashSet<EventTransportAllocationMap>();
            StaffRouteMonthlySplits = new HashSet<StaffRouteMonthlySplit>();
            StaffRouteShiftMapLogs = new HashSet<StaffRouteShiftMapLog>();
        }

        [Key]
        public long StaffRouteStopMapIID { get; set; }
        public long? StaffID { get; set; }
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
        public bool? TermsAndConditions { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? TransportStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelDate { get; set; }
        public int? IsRouteShifted { get; set; }
        public int? ShiftFromRouteID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StaffRouteStopMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("DropStopMapID")]
        [InverseProperty("StaffRouteStopMapDropStopMaps")]
        public virtual RouteStopMap DropStopMap { get; set; }
        [ForeignKey("DropStopRouteID")]
        [InverseProperty("StaffRouteStopMapDropStopRoutes")]
        public virtual Route1 DropStopRoute { get; set; }
        [ForeignKey("PickupRouteID")]
        [InverseProperty("StaffRouteStopMapPickupRoutes")]
        public virtual Route1 PickupRoute { get; set; }
        [ForeignKey("PickupStopMapID")]
        [InverseProperty("StaffRouteStopMapPickupStopMaps")]
        public virtual RouteStopMap PickupStopMap { get; set; }
        [ForeignKey("RouteStopMapID")]
        [InverseProperty("StaffRouteStopMapRouteStopMaps")]
        public virtual RouteStopMap RouteStopMap { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StaffRouteStopMaps")]
        public virtual School School { get; set; }
        [ForeignKey("StaffID")]
        [InverseProperty("StaffRouteStopMaps")]
        public virtual Employee Staff { get; set; }
        [ForeignKey("TransportStatusID")]
        [InverseProperty("StaffRouteStopMaps")]
        public virtual TransportStatus TransportStatus { get; set; }
        [InverseProperty("StaffRouteStopMap")]
        public virtual ICollection<EventTransportAllocationMap> EventTransportAllocationMaps { get; set; }
        [InverseProperty("StaffRouteStopMap")]
        public virtual ICollection<StaffRouteMonthlySplit> StaffRouteMonthlySplits { get; set; }
        [InverseProperty("StaffRouteStopMap")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogs { get; set; }
    }
}
