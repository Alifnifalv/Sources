namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FeeDueMonthlySplit151804")]
    public partial class FeeDueMonthlySplit151804
    {
        [Key]
        [Column(Order = 0)]
        public long FeeDueMonthlySplitIID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FeeDueFeeTypeMapsID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MonthID { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxPercentage { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxAmount { get; set; }

        public int? FeePeriodID { get; set; }

        public int? Year { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool Status { get; set; }

        public long? FeeStructureMontlySplitMapID { get; set; }

        public long? AccountTransactionHeadID { get; set; }
    }
}
