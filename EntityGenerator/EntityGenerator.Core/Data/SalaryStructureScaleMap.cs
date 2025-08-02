using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryStructureScaleMaps", Schema = "payroll")]
    public partial class SalaryStructureScaleMap
    {
        [Key]
        public long StructureScaleID { get; set; }
        public bool? IsSponsored { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? MinAmount { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? MaxAmount { get; set; }
        public int? AccomodationTypeID { get; set; }
        public string IncrementNote { get; set; }
        public int? MaritalStatusID { get; set; }
        [StringLength(250)]
        public string LeaveTicket { get; set; }
        public long? SalaryStructureID { get; set; }

        [ForeignKey("AccomodationTypeID")]
        [InverseProperty("SalaryStructureScaleMaps")]
        public virtual AccomodationType AccomodationType { get; set; }
        [ForeignKey("MaritalStatusID")]
        [InverseProperty("SalaryStructureScaleMaps")]
        public virtual MaritalStatus1 MaritalStatus { get; set; }
        [ForeignKey("SalaryStructureID")]
        [InverseProperty("SalaryStructureScaleMaps")]
        public virtual SalaryStructure SalaryStructure { get; set; }
    }
}
