using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VehicleOwnershipTypes", Schema = "mutual")]
    public partial class VehicleOwnershipType
    {
        public VehicleOwnershipType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        public short VehicleOwnershipTypeID { get; set; }
        [StringLength(50)]
        public string OwnershipTypeName { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("VehicleOwnershipType")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
