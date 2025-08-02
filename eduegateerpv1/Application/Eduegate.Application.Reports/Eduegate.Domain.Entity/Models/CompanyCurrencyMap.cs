using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CompanyCurrencyMap
    {
        public int CompanyID { get; set; }
        public int CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual Company Company { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
