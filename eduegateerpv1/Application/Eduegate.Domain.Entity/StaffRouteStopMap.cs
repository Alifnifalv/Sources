namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StaffRouteStopMaps")]
    public partial class StaffRouteStopMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? PickupRouteID { get; set; }

        public int? DropStopRouteID { get; set; }

        public bool? TermsAndConditions { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public long? TransportStatusID { get; set; }

        public DateTime? CancelDate { get; set; }

        public int? IsRouteShifted { get; set; }

        public int? ShiftFromRouteID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventTransportAllocationMap> EventTransportAllocationMaps { get; set; }

        public virtual Routes1 Routes1 { get; set; }

        public virtual Routes1 Routes11 { get; set; }

        public virtual RouteStopMap RouteStopMap { get; set; }

        public virtual RouteStopMap RouteStopMap1 { get; set; }

        public virtual RouteStopMap RouteStopMap2 { get; set; }

        public virtual School School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffRouteMonthlySplit> StaffRouteMonthlySplits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogs { get; set; }

        public virtual TransportStatus TransportStatus { get; set; }
    }
}
