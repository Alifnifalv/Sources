using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("Banks", Schema = "mutual")]
    public partial class Bank
    {
        public Bank()
        {
            BankAccounts = new HashSet<BankAccount>();
        }

        [Key]
        public long BankIID { get; set; }

        [StringLength(10)]
        public string BankCode { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string ShortName { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
