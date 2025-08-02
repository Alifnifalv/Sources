using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("CompanyCurrencyMaps", Schema = "mutual")]
    public partial class CompanyCurrencyMap
    {
        [Key]
        public int CompanyID { get; set; }
        public int CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual Company Company { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
