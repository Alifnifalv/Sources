using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RouteVehicleMaps", Schema = "schools")]
    public partial class RouteVehicleMap
    {
        [Key]
        public long RouteVehicleMapIID { get; set; }
        public int? RouteID { get; set; }
        public long? VehicleID { get; set; }
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
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("RouteVehicleMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("RouteID")]
        [InverseProperty("RouteVehicleMaps")]
        public virtual Route1 Route { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("RouteVehicleMaps")]
        public virtual School School { get; set; }
        [ForeignKey("VehicleID")]
        [InverseProperty("RouteVehicleMaps")]
        public virtual Vehicle Vehicle { get; set; }
    }
}
