using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VehicleTypes", Schema = "mutual")]
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        public short VehicleTypeID { get; set; }
        [StringLength(50)]
        public string VehicleTypeName { get; set; }
        [StringLength(50)]
        public string Capacity { get; set; }
        [StringLength(100)]
        public string Dimensions { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("VehicleType")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
