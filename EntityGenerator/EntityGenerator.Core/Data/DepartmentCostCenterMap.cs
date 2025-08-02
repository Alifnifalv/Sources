using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("DepartmentCostCenterMaps", Schema = "account")]
    public partial class DepartmentCostCenterMap
    {
        public long DepartmentCostCenterMapIID { get; set; }
        public long? DepartmentID { get; set; }
        public int CostCenterID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CostCenterID")]
        public virtual CostCenter CostCenter { get; set; }
        [ForeignKey("DepartmentID")]
        public virtual Department1 Department { get; set; }
    }
}
