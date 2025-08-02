using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TRANPORT_COLL_DATA_20220621
    {
        public long FeeCollectionFeeTypeMapId { get; set; }
        public int MonthID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int TaxPercentage { get; set; }
        public int TaxAmount { get; set; }
        public int? FeePeriodID { get; set; }
        public long FeeDueMonthlySplitID { get; set; }
        public int? Year { get; set; }
        public int CreditNoteAmount { get; set; }
        public int Balance { get; set; }
        public int RefundAmount { get; set; }
        public int? AccountTransactionHeadID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DueAmount { get; set; }
    }
}
