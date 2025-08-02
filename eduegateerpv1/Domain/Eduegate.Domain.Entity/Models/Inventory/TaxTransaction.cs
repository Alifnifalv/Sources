using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Inventory
{
    [Table("TaxTransactions", Schema = "inventory")]
    public partial class TaxTransaction
    {
        [Key]
        public long TaxTransactionIID { get; set; }
        public Nullable<long> HeadID { get; set; }
        public Nullable<int> TaxTemplateItemID { get; set; }
        public Nullable<int> TaxTemplateID { get; set; }
        public Nullable<int> TaxTypeID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<long> AccoundID { get; set; }
        public Nullable<int> Percentage { get; set; }
        public Nullable<bool> HasTaxInclusive { get; set; }
        public Nullable<decimal> InclusiveAmount { get; set; }
        public Nullable<decimal> ExclusiveAmount { get; set; }
        public virtual Account Account { get; set; }
        public virtual TaxTemplateItem TaxTemplateItem { get; set; }
        public virtual TaxTemplate TaxTemplate { get; set; }
        public virtual TaxType TaxType { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
