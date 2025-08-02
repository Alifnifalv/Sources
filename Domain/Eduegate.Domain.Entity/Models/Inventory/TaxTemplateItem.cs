using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Inventory
{
    [Table("TaxTemplateItems", Schema = "inventory")]
    public partial class TaxTemplateItem
    {
        public TaxTemplateItem()
        {
            this.TaxTransactions = new List<TaxTransaction>();
        }

        [Key]
        public int TaxTemplateItemID { get; set; }
        public Nullable<int> TaxTemplateID { get; set; }
        public Nullable<int> TaxTypeID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> Percentage { get; set; }
        public Nullable<bool> HasTaxInclusive { get; set; }
        public virtual Account Account { get; set; }
        public virtual TaxTemplate TaxTemplate { get; set; }
        public virtual TaxType TaxType { get; set; }
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }
    }
}
