using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Inventory
{
    [Table("Taxes", Schema = "inventory")]
    public partial class Tax
    {
        [Key]
        public long TaxID { get; set; }

        [StringLength(50)]
        public string TaxName { get; set; }

        public int? TaxTypeID { get; set; }

        public long? AccountID { get; set; }

        public int? Percentage { get; set; }

        public decimal? Amount { get; set; }

        public int? TaxStatusID { get; set; }

        public virtual Account Account { get; set; }

        public virtual TaxStatus TaxStatus { get; set; }

        public virtual TaxType TaxType { get; set; }
    }
}