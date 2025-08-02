using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("Currencies", Schema = "mutual")]
    public partial class Currency
    {
        public Currency()
        {
            BankAccounts = new HashSet<BankAccount>();
            Countries = new HashSet<Country>();
            Payables = new HashSet<Payable>();
            Receivables = new HashSet<Receivable>();
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

        public decimal? ExchangeRate { get; set; }

        public bool? IsEnabled { get; set; }

        [StringLength(20)]
        public string DisplayCode { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }

        public virtual ICollection<Country> Countries { get; set; }

        public virtual ICollection<Payable> Payables { get; set; }

        public virtual ICollection<Receivable> Receivables { get; set; }
    }
}