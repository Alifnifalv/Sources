namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentVehicleAssign", Schema = "schools")]
    public partial class StudentVehicleAssign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long StudentAssignId { get; set; }

        public long VehicleId { get; set; }

        public int RouteId { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Routes1 Routes1 { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
