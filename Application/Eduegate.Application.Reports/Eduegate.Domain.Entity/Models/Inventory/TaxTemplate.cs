using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models.Inventory
{
    public partial class TaxTemplate
    {
        public TaxTemplate()
        {
            this.TaxTemplateItems = new List<TaxTemplateItem>();
            this.DocumentTypes = new List<DocumentType>();
            this.TaxTransactions = new List<TaxTransaction>();
        }

        public int TaxTemplateID { get; set; }
        public string TemplateName { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> HasTaxInclusive { get; set; }
        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }
    }
}
