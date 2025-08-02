namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.Vehicles")]
    public partial class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long VehicleID { get; set; }

        [StringLength(50)]
        public string VehicleNumber { get; set; }

        public int? YearMade { get; set; }

        [StringLength(50)]
        public string DriverName { get; set; }

        [StringLength(50)]
        public string DriverLicense { get; set; }

        [StringLength(50)]
        public string DriverContactNumber { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }
    }
}
