using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Inventory
{
    [Table("TaxTypes", Schema = "inventory")]
    public partial class TaxType
    {
        public TaxType()
        {
            AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            TaxTemplateItems = new HashSet<TaxTemplateItem>();
            TaxTransactions = new HashSet<TaxTransaction>();
            Taxes = new HashSet<Tax>();
        }

        [Key]
        public int TaxTypeID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }

        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }

        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }

        public virtual ICollection<Tax> Taxes { get; set; }
    }
}