using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Inventory;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
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

        public decimal? Amount { get; set; }

        public long? AccoundID { get; set; }

        public int? Percentage { get; set; }

        public virtual Account Accound { get; set; }

        public virtual AccountTransactionHead Head { get; set; }

        public virtual TaxTemplate TaxTemplate { get; set; }

        public virtual TaxTemplateItem TaxTemplateItem { get; set; }

        public virtual TaxType TaxType { get; set; }
    }
}