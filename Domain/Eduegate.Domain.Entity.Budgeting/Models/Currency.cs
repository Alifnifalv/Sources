using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
{
    [Table("Currencies", Schema = "mutual")]
    public partial class Currency
    {
        public Currency()
        {
            Budget1 = new HashSet<Budget1>();
            Companies = new HashSet<Company>();
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

        public virtual ICollection<Budget1> Budget1 { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}