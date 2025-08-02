using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("BankAccountStatuses", Schema = "account")]
    [Index("StatusName", Name = "UQ__BankAcco__05E7698AAEEEBD1B", IsUnique = true)]
    public partial class BankAccountStatus
    {
        public BankAccountStatus()
        {
            BankAccounts = new HashSet<BankAccount>();
        }

        [Key]
        public int StatusID { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string StatusName { get; set; }

        [StringLength(2000)]
        [Unicode(false)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}