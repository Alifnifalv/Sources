using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeDueFeeTypeMaps_20220806
    {
        public long FeeDueFeeTypeMapsIID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long? ClassFeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? TaxPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TaxAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool Status { get; set; }
        public int? FeeMasterID { get; set; }
        public long? FeeMasterClassMapID { get; set; }
        public long? FineMasterStudentMapID { get; set; }
        public int? FineMasterID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CollectedAmount { get; set; }
        public long? FeeStructureFeeMapID { get; set; }
        public long? AccountTransactionHeadID { get; set; }
    }
}
