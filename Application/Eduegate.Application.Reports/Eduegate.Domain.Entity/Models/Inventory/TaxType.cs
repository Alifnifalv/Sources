using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models.Inventory
{
    public partial class TaxType
    {
        public TaxType()
        {
            this.Taxes = new List<Tax>();
            this.TaxTemplateItems = new List<TaxTemplateItem>();
            this.TaxTransactions = new List<TaxTransaction>();
            this.AccountTaxTransactions = new List<AccountTaxTransaction>();
        }

        public int TaxTypeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }
    }
}
