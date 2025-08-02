using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssignVehicleAttendantMaps", Schema = "mutual")]
    public partial class AssignVehicleAttendantMap
    {
        [Key]
        public long AssignVehicleAttendantMapIID { get; set; }
        public long AssignVehicleMapID { get; set; }
        public long? EmployeeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AssignVehicleMapID")]
        [InverseProperty("AssignVehicleAttendantMaps")]
        public virtual AssignVehicleMap AssignVehicleMap { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("AssignVehicleAttendantMaps")]
        public virtual Employee Employee { get; set; }
    }
}
