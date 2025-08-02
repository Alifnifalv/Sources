using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RouteStopMaps", Schema = "schools")]
    public partial class RouteStopMap
    {
        public RouteStopMap()
        {
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
            StaffRouteShiftMapLogDropStopMaps = new HashSet<StaffRouteShiftMapLog>();
            StaffRouteShiftMapLogPickupStopMaps = new HashSet<StaffRouteShiftMapLog>();
            StaffRouteStopMapDropStopMaps = new HashSet<StaffRouteStopMap>();
            StaffRouteStopMapPickupStopMaps = new HashSet<StaffRouteStopMap>();
            StaffRouteStopMapRouteStopMaps = new HashSet<StaffRouteStopMap>();
            StudentRouteStopMapDropStopMaps = new HashSet<StudentRouteStopMap>();
            StudentRouteStopMapLogDropStopMaps = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMapLogPickupStopMaps = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMapLogRouteStopMaps = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMapPickupStopMaps = new HashSet<StudentRouteStopMap>();
            StudentRouteStopMapRouteStopMaps = new HashSet<StudentRouteStopMap>();
            TransportApplicationDropStopMaps = new HashSet<TransportApplication>();
            TransportApplicationPickupStopMaps = new HashSet<TransportApplication>();
        }

        [Key]
        public long RouteStopMapIID { get; set; }
        public int RouteID { get; set; }
        [StringLength(50)]
        public string StopName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OneWayFee { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TwoWayFee { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? Created { get; set; }
        public int? Updated { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(20)]
        public string StopCode { get; set; }
        public bool? IsActive { get; set; }
        public int? SequenceNo { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("RouteStopMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("RouteID")]
        [InverseProperty("RouteStopMaps")]
        public virtual Route1 Route { get; set; }
        [InverseProperty("RouteStopMap")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }
        [InverseProperty("DropStopMap")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogDropStopMaps { get; set; }
        [InverseProperty("PickupStopMap")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogPickupStopMaps { get; set; }
        [InverseProperty("DropStopMap")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMapDropStopMaps { get; set; }
        [InverseProperty("PickupStopMap")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMapPickupStopMaps { get; set; }
        [InverseProperty("RouteStopMap")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMapRouteStopMaps { get; set; }
        [InverseProperty("DropStopMap")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMapDropStopMaps { get; set; }
        [InverseProperty("DropStopMap")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogDropStopMaps { get; set; }
        [InverseProperty("PickupStopMap")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogPickupStopMaps { get; set; }
        [InverseProperty("RouteStopMap")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogRouteStopMaps { get; set; }
        [InverseProperty("PickupStopMap")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMapPickupStopMaps { get; set; }
        [InverseProperty("RouteStopMap")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMapRouteStopMaps { get; set; }
        [InverseProperty("DropStopMap")]
        public virtual ICollection<TransportApplication> TransportApplicationDropStopMaps { get; set; }
        [InverseProperty("PickupStopMap")]
        public virtual ICollection<TransportApplication> TransportApplicationPickupStopMaps { get; set; }
    }
}
