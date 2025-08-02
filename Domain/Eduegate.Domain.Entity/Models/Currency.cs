using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Currencies", Schema = "mutual")]
    public partial class Currency
    {
        public Currency()
        {
            this.InvetoryTransactions = new List<InvetoryTransaction>();
            this.TransactionHeads = new List<TransactionHead>();
            this.Companies = new List<Company>();
            this.CompanyCurrencyMaps = new List<CompanyCurrencyMap>();
            this.Payables = new List<Payable>();
            this.Receivables = new List<Receivable>();
            BankAccounts = new HashSet<BankAccount>();
        }

        [Key]
        public int CurrencyID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string ISOCode { get; set; }
        public string AnsiCode { get; set; }
        public Nullable<int> NumericCode { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Nullable<byte> DecimalPrecisions { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public string DisplayCode { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<CompanyCurrencyMap> CompanyCurrencyMaps { get; set; }
        public virtual ICollection<Payable> Payables { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
