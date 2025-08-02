using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Budgets", Schema = "account")]
    public partial class Budget
    {
        public Budget()
        {
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            AccountTransactions = new HashSet<AccountTransaction>();
        }

        [Key]
        public int BudgetID { get; set; }
        [StringLength(50)]
        public string BudgetCode { get; set; }
        [StringLength(100)]
        public string BudgetName { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("Budget")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        [InverseProperty("Budget")]
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
    }
}
