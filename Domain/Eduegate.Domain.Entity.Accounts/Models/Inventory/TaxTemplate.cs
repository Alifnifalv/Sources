using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Catalog;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;

namespace Eduegate.Domain.Entity.Accounts.Models.Inventory
{
    [Table("TaxTemplates", Schema = "inventory")]
    public partial class TaxTemplate
    {
        public TaxTemplate()
        {
            AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            DocumentTypes = new HashSet<DocumentType>();
            Products = new HashSet<Product>();
            TaxTemplateItems = new HashSet<TaxTemplateItem>();
            TaxTransactions = new HashSet<TaxTransaction>();
        }

        [Key]
        public int TaxTemplateID { get; set; }

        [StringLength(50)]
        public string TemplateName { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsActive { get; set; }

        public bool? HasTaxInclusive { get; set; }

        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }

        public virtual ICollection<DocumentType> DocumentTypes { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }

        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
    }
}