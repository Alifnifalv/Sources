using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Currencies", Schema = "mutual")]
    public partial class Currency
    {
        public Currency()
        {
            BankAccounts = new HashSet<BankAccount>();
            Budget1 = new HashSet<Budget1>();
            Companies = new HashSet<Company>();
            CompanyCurrencyMaps = new HashSet<CompanyCurrencyMap>();
            Countries = new HashSet<Country>();
            InvetoryTransactions = new HashSet<InvetoryTransaction>();
            Payables = new HashSet<Payable>();
            Receivables = new HashSet<Receivable>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
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
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? ExchangeRate { get; set; }
        public bool? IsEnabled { get; set; }
        [StringLength(20)]
        public string DisplayCode { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("Currencies")]
        public virtual Company Company { get; set; }
        [InverseProperty("Currency")]
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        [InverseProperty("Currency")]
        public virtual ICollection<Budget1> Budget1 { get; set; }
        [InverseProperty("BaseCurrency")]
        public virtual ICollection<Company> Companies { get; set; }
        [InverseProperty("Currency")]
        public virtual ICollection<CompanyCurrencyMap> CompanyCurrencyMaps { get; set; }
        [InverseProperty("Currency")]
        public virtual ICollection<Country> Countries { get; set; }
        [InverseProperty("Currency")]
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        [InverseProperty("Currency")]
        public virtual ICollection<Payable> Payables { get; set; }
        [InverseProperty("Currency")]
        public virtual ICollection<Receivable> Receivables { get; set; }
        [InverseProperty("Currency")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
