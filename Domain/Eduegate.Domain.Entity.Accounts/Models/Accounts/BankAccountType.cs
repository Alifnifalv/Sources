using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("BankAccountTypes", Schema = "account")]
    [Index("AccountTypeName", Name = "UQ__BankAcco__602E1DF2EFDFC537", IsUnique = true)]
    public partial class BankAccountType
    {
        public BankAccountType()
        {
            BankAccounts = new HashSet<BankAccount>();
        }

        [Key]
        public int AccountTypeID { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string AccountTypeName { get; set; }

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