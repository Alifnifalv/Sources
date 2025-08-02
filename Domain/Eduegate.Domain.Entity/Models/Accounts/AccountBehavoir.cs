using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AccountBehavoirs", Schema = "account")]
    public partial class AccountBehavoir
    {
        public AccountBehavoir()
        {
            this.Accounts = new List<Account>();
        }

        [Key]
        public byte AccountBehavoirID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
