using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BankReconciliationMatchedStatuses", Schema = "account")]
    public partial class BankReconciliationMatchedStatus
    {
        public BankReconciliationMatchedStatus()
        {
            BankReconciliationDetails = new HashSet<BankReconciliationDetail>();
        }

        [Key]
        public short StatusID { get; set; }
        [Required]
        [StringLength(250)]
        public string StatusName { get; set; }

        [InverseProperty("BankReconciliationMatchedStatus")]
        public virtual ICollection<BankReconciliationDetail> BankReconciliationDetails { get; set; }
    }
}
