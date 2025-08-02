namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.RouteVehicleMaps")]
    public partial class RouteVehicleMap
    {
        [Key]
        public long RouteVehicleMapIID { get; set; }

        public int? RouteID { get; set; }

        public long? VehicleID { get; set; }

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

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Routes1 Routes1 { get; set; }

        public virtual School School { get; set; }
    }
}
