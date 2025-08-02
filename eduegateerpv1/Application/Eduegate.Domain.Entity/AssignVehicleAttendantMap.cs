namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.AssignVehicleAttendantMaps")]
    public partial class AssignVehicleAttendantMap
    {
        [Key]
        public long AssignVehicleAttendantMapIID { get; set; }

        public long AssignVehicleMapID { get; set; }

        public long? EmployeeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual AssignVehicleMap AssignVehicleMap { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
