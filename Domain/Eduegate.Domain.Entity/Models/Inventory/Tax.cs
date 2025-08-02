using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Inventory
{
    [Table("Taxes", Schema = "inventory")]
    public partial class Tax
    {
        [Key]
        public long TaxID { get; set; }
        public string TaxName { get; set; }
        public Nullable<int> TaxTypeID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<int> Percentage { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> TaxStatusID { get; set; }
        public virtual Account Account { get; set; }
        public virtual TaxStatus TaxStatus { get; set; }
        public virtual TaxType TaxType { get; set; }
    }
}
