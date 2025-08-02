using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Inventory
{
    [Table("TaxTemplateItems", Schema = "inventory")]
    public partial class TaxTemplateItem
    {
        public TaxTemplateItem()
        {
            AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            TaxTransactions = new HashSet<TaxTransaction>();
        }

        [Key]
        public int TaxTemplateItemID { get; set; }

        public int? TaxTemplateID { get; set; }

        public int? TaxTypeID { get; set; }

        public long? AccountID { get; set; }

        public decimal? Amount { get; set; }

        public int? Percentage { get; set; }

        public bool? HasTaxInclusive { get; set; }

        public virtual Account Account { get; set; }

        public virtual TaxTemplate TaxTemplate { get; set; }

        public virtual TaxType TaxType { get; set; }

        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }

        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
    }
}