namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.DepartmentCostCenterMaps")]
    public partial class DepartmentCostCenterMap
    {
        [Key]
       
        public long DepartmentCostCenterMapIID { get; set; }

        public long? DepartmentID { get; set; }

      
        public int CostCenterID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual CostCenter CostCenter { get; set; }

        public virtual Department Department { get; set; }
    }
}
