using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeStructureMontlySplitMaps", Schema = "schools")]
    public partial class FeeStructureMontlySplitMap
    {
        public FeeStructureMontlySplitMap()
        {
            FeeDueMonthlySplits = new HashSet<FeeDueMonthlySplit>();
        }

        [Key]
        public long FeeStructureMontlySplitMapIID { get; set; }
        public long? FeeStructureFeeMapID { get; set; }
        public byte? MonthID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? TaxPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        public int? Year { get; set; }

        [ForeignKey("FeeStructureFeeMapID")]
        [InverseProperty("FeeStructureMontlySplitMaps")]
        public virtual FeeStructureFeeMap FeeStructureFeeMap { get; set; }
        [InverseProperty("FeeStructureMontlySplitMap")]
        public virtual ICollection<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }
    }
}
