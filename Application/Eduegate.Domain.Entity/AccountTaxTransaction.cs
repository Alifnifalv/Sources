namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.AccountTaxTransactions")]
    public partial class AccountTaxTransaction
    {
        [Key]
        public long TaxTransactionIID { get; set; }

        public long? HeadID { get; set; }

        public int? TaxTemplateItemID { get; set; }

        public int? TaxTemplateID { get; set; }

        public int? TaxTypeID { get; set; }

        public decimal? Amount { get; set; }

        public long? AccoundID { get; set; }

        public int? Percentage { get; set; }

        public virtual Account Account { get; set; }

        public virtual TaxTemplateItem TaxTemplateItem { get; set; }

        public virtual TaxTemplate TaxTemplate { get; set; }

        public virtual TaxType TaxType { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
    }
}
