using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TaxTransactions", Schema = "inventory")]
    public partial class TaxTransaction
    {
        [Key]
        public long TaxTransactionIID { get; set; }
        public long? HeadID { get; set; }
        public int? TaxTemplateItemID { get; set; }
        public int? TaxTemplateID { get; set; }
        public int? TaxTypeID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? InclusiveAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExclusiveAmount { get; set; }
        public long? AccoundID { get; set; }
        public int? Percentage { get; set; }
        public bool? HasTaxInclusive { get; set; }

        [ForeignKey("AccoundID")]
        [InverseProperty("TaxTransactions")]
        public virtual Account Accound { get; set; }
        [ForeignKey("HeadID")]
        [InverseProperty("TaxTransactions")]
        public virtual TransactionHead Head { get; set; }
        [ForeignKey("TaxTemplateID")]
        [InverseProperty("TaxTransactions")]
        public virtual TaxTemplate TaxTemplate { get; set; }
        [ForeignKey("TaxTemplateItemID")]
        [InverseProperty("TaxTransactions")]
        public virtual TaxTemplateItem TaxTemplateItem { get; set; }
        [ForeignKey("TaxTypeID")]
        [InverseProperty("TaxTransactions")]
        public virtual TaxType TaxType { get; set; }
    }
}
