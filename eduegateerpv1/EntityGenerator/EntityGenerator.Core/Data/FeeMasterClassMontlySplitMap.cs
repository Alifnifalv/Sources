using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeMasterClassMontlySplitMaps", Schema = "schools")]
    public partial class FeeMasterClassMontlySplitMap
    {
        [Key]
        public long FeeMasterClassMontlySplitMapIID { get; set; }
        public long? FeeMasterClassMapID { get; set; }
        public byte? MonthID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? TaxPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        public int? FeePeriodID { get; set; }
        public int? Year { get; set; }

        [ForeignKey("FeeMasterClassMapID")]
        [InverseProperty("FeeMasterClassMontlySplitMaps")]
        public virtual FeeMasterClassMap FeeMasterClassMap { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("FeeMasterClassMontlySplitMaps")]
        public virtual FeePeriod FeePeriod { get; set; }
    }
}
