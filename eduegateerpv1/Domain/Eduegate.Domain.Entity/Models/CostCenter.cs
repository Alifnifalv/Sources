using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("CostCenters", Schema = "account")]
    public partial class CostCenter
    {
        public CostCenter()
        {
            this.AccountTransactions = new List<AccountTransaction>();
            this.TransactionDetails = new HashSet<TransactionDetail>();
            this.CostCenterAccountMaps = new HashSet<CostCenterAccountMap>();
        }

        [Key]
        public int CostCenterID { get; set; }
        public string CostCenterName { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostCenterAccountMap> CostCenterAccountMaps { get; set; }
    }
}
