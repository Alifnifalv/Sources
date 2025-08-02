using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public class TaxDetailViewModel
    {
        public long TaxID { get; set; }
        public int? TaxTemplateID { get; set; }
        public int? TaxTemplateItemID { get; set; }
        public int? TaxTypeID { get; set; }
        public long? AccountID { get; set; }
        public decimal? Amount { get; set; }
        public int? Percentage { get; set; }
        public bool? HasTaxInclusive { get; set; }
        public string TaxName { get; set; }
        public decimal? InclusiveTaxAmount { get; set; }
        public decimal? ExclusiveTaxAmount { get; set; }
    }
}
