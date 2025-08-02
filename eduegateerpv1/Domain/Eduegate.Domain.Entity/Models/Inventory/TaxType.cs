using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Inventory
{
    [Table("TaxTypes", Schema = "inventory")]
    public partial class TaxType
    {
        public TaxType()
        {
            this.Taxes = new List<Tax>();
            this.TaxTemplateItems = new List<TaxTemplateItem>();
            this.TaxTransactions = new List<TaxTransaction>();
            this.AccountTaxTransactions = new List<AccountTaxTransaction>();
        }

        [Key]
        public int TaxTypeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }
    }
}
