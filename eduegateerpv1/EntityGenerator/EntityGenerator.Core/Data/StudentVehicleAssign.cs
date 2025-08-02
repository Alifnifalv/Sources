using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentVehicleAssign", Schema = "schools")]
    public partial class StudentVehicleAssign
    {
        [Key]
        public long StudentAssignId { get; set; }
        public long VehicleId { get; set; }
        public int RouteId { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("RouteId")]
        [InverseProperty("StudentVehicleAssigns")]
        public virtual Route1 Route { get; set; }
        [ForeignKey("VehicleId")]
        [InverseProperty("StudentVehicleAssigns")]
        public virtual Vehicle Vehicle { get; set; }
    }
}
