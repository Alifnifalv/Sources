namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AdditionalExpensesTransactionsMaps", Schema = "account")]
    public partial class AdditionalExpensesTransactionsMap
    {
        [Key]
        public long AdditionalExpensesTransactionsMapIID { get; set; }

        public int? AdditionalExpenseID { get; set; }

        public long? AccountID { get; set; }

        public long? ProvisionalAccountID { get; set; }

        public int? ForeignCurrencyID { get; set; }

        public int? LocalCurrencyID { get; set; }

        [Column(TypeName = "money")]
        public decimal? ExchangeRate { get; set; }

        [Column(TypeName = "money")]
        public decimal? ForeignAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal? LocalAmount { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte? CompanyID { get; set; }

        public byte? FiscalYearID { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public long? RefInventoryTransactionHeadID { get; set; }

        public long? RefAccountTransactionHeadID { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        public bool? ISAffectSupplier { get; set; }
    }
}
