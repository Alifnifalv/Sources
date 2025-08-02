namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("Budgets", Schema = "account")]
    public partial class Budget
    {
        public Budget()
        {
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            AccountTransactions = new HashSet<AccountTransaction>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BudgetID { get; set; }

        [StringLength(50)]
        public string BudgetCode { get; set; }

        [StringLength(100)]
        public string BudgetName { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
    }
}
