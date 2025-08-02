using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Routes", Schema = "schools")]
    public partial class Route1
    {
        public Route1()
        {
            AssignVehicleMaps = new HashSet<AssignVehicleMap>();
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
            EventTransportAllocationMaps = new HashSet<EventTransportAllocationMap>();
            RouteStopMaps = new HashSet<RouteStopMap>();
            RouteVehicleMaps = new HashSet<RouteVehicleMap>();
            StaffRouteShiftMapLogs = new HashSet<StaffRouteShiftMapLog>();
            StaffRouteStopMapDropStopRoutes = new HashSet<StaffRouteStopMap>();
            StaffRouteStopMapPickupRoutes = new HashSet<StaffRouteStopMap>();
            StudentRouteStopMapDropStopRoutes = new HashSet<StudentRouteStopMap>();
            StudentRouteStopMapLogDropStopRoutes = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMapLogPickupRoutes = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMapLogRoutes = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMapPickupRoutes = new HashSet<StudentRouteStopMap>();
            StudentVehicleAssigns = new HashSet<StudentVehicleAssign>();
            VehicleTrackings = new HashSet<VehicleTracking>();
        }

        [Key]
        public int RouteID { get; set; }
        [StringLength(100)]
        public string RouteCode { get; set; }
        public byte? RouteTypeID { get; set; }
        [StringLength(50)]
        public string RouteDescription { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RouteFareOneWay { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RouteFareTwoWay { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CostCenterID { get; set; }
        [StringLength(50)]
        public string Landmark { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(15)]
        public string ContactNumber { get; set; }
        public bool? IsActive { get; set; }
        public int? RouteGroupID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Route1")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("CostCenterID")]
        [InverseProperty("Route1")]
        public virtual CostCenter CostCenter { get; set; }
        [ForeignKey("RouteGroupID")]
        [InverseProperty("Route1")]
        public virtual RouteGroup RouteGroup { get; set; }
        [ForeignKey("RouteTypeID")]
        [InverseProperty("Route1")]
        public virtual RouteType RouteType { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Route1")]
        public virtual School School { get; set; }
        [InverseProperty("Route")]
        public virtual ICollection<AssignVehicleMap> AssignVehicleMaps { get; set; }
        [InverseProperty("Route")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }
        [InverseProperty("ToRoute")]
        public virtual ICollection<EventTransportAllocationMap> EventTransportAllocationMaps { get; set; }
        [InverseProperty("Route")]
        public virtual ICollection<RouteStopMap> RouteStopMaps { get; set; }
        [InverseProperty("Route")]
        public virtual ICollection<RouteVehicleMap> RouteVehicleMaps { get; set; }
        [InverseProperty("ShiftFromRoute")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogs { get; set; }
        [InverseProperty("DropStopRoute")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMapDropStopRoutes { get; set; }
        [InverseProperty("PickupRoute")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMapPickupRoutes { get; set; }
        [InverseProperty("DropStopRoute")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMapDropStopRoutes { get; set; }
        [InverseProperty("DropStopRoute")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogDropStopRoutes { get; set; }
        [InverseProperty("PickupRoute")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogPickupRoutes { get; set; }
        [InverseProperty("Route")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogRoutes { get; set; }
        [InverseProperty("PickupRoute")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMapPickupRoutes { get; set; }
        [InverseProperty("Route")]
        public virtual ICollection<StudentVehicleAssign> StudentVehicleAssigns { get; set; }
        [InverseProperty("Route")]
        public virtual ICollection<VehicleTracking> VehicleTrackings { get; set; }
    }
}
