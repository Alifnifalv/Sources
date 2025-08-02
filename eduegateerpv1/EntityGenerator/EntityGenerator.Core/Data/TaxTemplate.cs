using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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

        [InverseProperty("TaxTemplate")]
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }
        [InverseProperty("TaxTemplate")]
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
        [InverseProperty("TaxTemplate")]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty("TaxTemplate")]
        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }
        [InverseProperty("TaxTemplate")]
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
    }
}
