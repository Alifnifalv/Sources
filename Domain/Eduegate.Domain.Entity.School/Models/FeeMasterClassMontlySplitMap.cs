namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("FeeMasterClassMontlySplitMaps", Schema = "schools")]
    public partial class FeeMasterClassMontlySplitMap
    {
        [Key]
        public long FeeMasterClassMontlySplitMapIID { get; set; }

        public long? FeeMasterClassMapID { get; set; }

        public byte? MonthID { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public int? FeePeriodID { get; set; }

        public int? Year { get; set; }

        public virtual FeeMasterClassMap FeeMasterClassMap { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }
    }
}


