using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TaxTemplateItems", Schema = "inventory")]
    public partial class TaxTemplateItem
    {
        public TaxTemplateItem()
        {
            AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            TaxTransactions = new HashSet<TaxTransaction>();
        }

        [Key]
        public int TaxTemplateItemID { get; set; }
        public int? TaxTemplateID { get; set; }
        public int? TaxTypeID { get; set; }
        public long? AccountID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int? Percentage { get; set; }
        public bool? HasTaxInclusive { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("TaxTemplateItems")]
        public virtual Account Account { get; set; }
        [ForeignKey("TaxTemplateID")]
        [InverseProperty("TaxTemplateItems")]
        public virtual TaxTemplate TaxTemplate { get; set; }
        [ForeignKey("TaxTypeID")]
        [InverseProperty("TaxTemplateItems")]
        public virtual TaxType TaxType { get; set; }
        [InverseProperty("TaxTemplateItem")]
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }
        [InverseProperty("TaxTemplateItem")]
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
    }
}
