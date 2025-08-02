namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TaxTemplateItems")]
    public partial class TaxTemplateItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaxTemplateItem()
        {
            AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            TaxTransactions = new HashSet<TaxTransaction>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaxTemplateItemID { get; set; }

        public int? TaxTemplateID { get; set; }

        public int? TaxTypeID { get; set; }

        public long? AccountID { get; set; }

        public decimal? Amount { get; set; }

        public int? Percentage { get; set; }

        public bool? HasTaxInclusive { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }

        public virtual TaxTemplate TaxTemplate { get; set; }

        public virtual TaxType TaxType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
    }
}
