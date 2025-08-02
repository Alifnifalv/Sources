using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VehicleTransmissions", Schema = "mutual")]
    public partial class VehicleTransmission
    {
        public VehicleTransmission()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        public byte VehicleTransmissionID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("Transmission")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
