namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TaxTemplates")]
    public partial class TaxTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaxTemplate()
        {
            AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            Products = new HashSet<Product>();
            TaxTemplateItems = new HashSet<TaxTemplateItem>();
            DocumentTypes = new HashSet<DocumentType>();
            TaxTransactions = new HashSet<TaxTransaction>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaxTemplateID { get; set; }

        [StringLength(50)]
        public string TemplateName { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsActive { get; set; }

        public bool? HasTaxInclusive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
    }
}
