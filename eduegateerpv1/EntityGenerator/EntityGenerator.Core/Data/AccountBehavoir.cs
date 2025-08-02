using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AccountBehavoirs", Schema = "account")]
    public partial class AccountBehavoir
    {
        public AccountBehavoir()
        {
            Accounts = new HashSet<Account>();
        }

        [Key]
        public byte AccountBehavoirID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("AccountBehavoir")]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
