using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BankReconciliationStatuses", Schema = "account")]
    public partial class BankReconciliationStatus
    {
        public BankReconciliationStatus()
        {
            BankReconciliationHeads = new HashSet<BankReconciliationHead>();
        }

        [Key]
        public short StatusID { get; set; }
        [Required]
        [StringLength(250)]
        public string StatusName { get; set; }

        [InverseProperty("BankReconciliationStatus")]
        public virtual ICollection<BankReconciliationHead> BankReconciliationHeads { get; set; }
    }
}
