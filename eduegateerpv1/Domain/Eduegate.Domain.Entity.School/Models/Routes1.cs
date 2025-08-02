using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("Routes", Schema = "schools")]
    public partial class Routes1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Routes1()
        {
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
            RouteStopMaps = new HashSet<RouteStopMap>();
            RouteVehicleMaps = new HashSet<RouteVehicleMap>();
            StaffRouteStopMaps = new HashSet<StaffRouteStopMap>();
            StaffRouteStopMaps1 = new HashSet<StaffRouteStopMap>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
            StudentRouteStopMaps1 = new HashSet<StudentRouteStopMap>();
            StudentVehicleAssigns = new HashSet<StudentVehicleAssign>();
            AssignVehicleMaps = new HashSet<AssignVehicleMap>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMapLogs1 = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMapLogs2 = new HashSet<StudentRouteStopMapLog>();
            StaffRouteShiftMapLogs = new HashSet<StaffRouteShiftMapLog>();
            EventTransportAllocationMaps = new HashSet<EventTransportAllocationMap>();
            VehicleTrackings = new HashSet<VehicleTracking>();

        }

        [Key]
        public int RouteID { get; set; }

        [StringLength(100)]
        public string RouteCode { get; set; }

        public byte? RouteTypeID { get; set; }

        [StringLength(50)]
        public string RouteDescription { get; set; }

        public decimal? RouteFareOneWay { get; set; }

        public decimal? RouteFareTwoWay { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public int? CostCenterID { get; set; }

        [StringLength(50)]
        public string Landmark { get; set; }

        public string ContactNumber { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public bool? IsActive { get; set; }

        public int? RouteGroupID { get; set; }

        public virtual RouteGroup RouteGroup { get; set; }

        public virtual CostCenter CostCenter { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual RouteType RouteType { get; set; }

        public virtual Schools School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteStopMap> RouteStopMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteVehicleMap> RouteVehicleMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentVehicleAssign> StudentVehicleAssigns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignVehicleMap> AssignVehicleMaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventTransportAllocationMap> EventTransportAllocationMaps { get; set; }

        public virtual ICollection<VehicleTracking> VehicleTrackings { get; set; }

    }
}