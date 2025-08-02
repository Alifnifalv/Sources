namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FeeDueFeeTypeMaps20221229_1")]
    public partial class FeeDueFeeTypeMaps20221229_1
    {
        [Key]
        [Column(Order = 0)]
        public long FeeDueFeeTypeMapsIID { get; set; }

        public long? StudentFeeDueID { get; set; }

        public long? ClassFeeMasterID { get; set; }

        public int? FeePeriodID { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxPercentage { get; set; }

        public decimal? TaxAmount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool Status { get; set; }

        public int? FeeMasterID { get; set; }

        public long? FeeMasterClassMapID { get; set; }

        public long? FineMasterStudentMapID { get; set; }

        public int? FineMasterID { get; set; }

        public decimal? CollectedAmount { get; set; }

        public long? FeeStructureFeeMapID { get; set; }

        public long? AccountTransactionHeadID { get; set; }
    }
}
