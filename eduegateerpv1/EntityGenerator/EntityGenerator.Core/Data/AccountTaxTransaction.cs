using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AccountTaxTransactions", Schema = "account")]
    public partial class AccountTaxTransaction
    {
        [Key]
        public long TaxTransactionIID { get; set; }
        public long? HeadID { get; set; }
        public int? TaxTemplateItemID { get; set; }
        public int? TaxTemplateID { get; set; }
        public int? TaxTypeID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public long? AccoundID { get; set; }
        public int? Percentage { get; set; }

        [ForeignKey("AccoundID")]
        [InverseProperty("AccountTaxTransactions")]
        public virtual Account Accound { get; set; }
        [ForeignKey("HeadID")]
        [InverseProperty("AccountTaxTransactions")]
        public virtual AccountTransactionHead Head { get; set; }
        [ForeignKey("TaxTemplateID")]
        [InverseProperty("AccountTaxTransactions")]
        public virtual TaxTemplate TaxTemplate { get; set; }
        [ForeignKey("TaxTemplateItemID")]
        [InverseProperty("AccountTaxTransactions")]
        public virtual TaxTemplateItem TaxTemplateItem { get; set; }
        [ForeignKey("TaxTypeID")]
        [InverseProperty("AccountTaxTransactions")]
        public virtual TaxType TaxType { get; set; }
    }
}
