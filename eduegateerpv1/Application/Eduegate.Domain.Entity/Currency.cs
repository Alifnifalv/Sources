namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Currencies")]
    public partial class Currency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Currency()
        {
            Payables = new HashSet<Payable>();
            Receivables = new HashSet<Receivable>();
            InvetoryTransactions = new HashSet<InvetoryTransaction>();
            TransactionHeads = new HashSet<TransactionHead>();
            Companies = new HashSet<Company>();
            CompanyCurrencyMaps = new HashSet<CompanyCurrencyMap>();
            Countries = new HashSet<Country>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CurrencyID { get; set; }

        public int? CompanyID { get; set; }

        [StringLength(20)]
        public string ISOCode { get; set; }

        [StringLength(20)]
        public string AnsiCode { get; set; }

        public int? NumericCode { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(5)]
        public string Symbol { get; set; }

        public byte? DecimalPrecisions { get; set; }

        public decimal? ExchangeRate { get; set; }

        public bool? IsEnabled { get; set; }

        [StringLength(20)]
        public string DisplayCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payable> Payables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receivable> Receivables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Company> Companies { get; set; }

        public virtual Company Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyCurrencyMap> CompanyCurrencyMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Country> Countries { get; set; }
    }
}
