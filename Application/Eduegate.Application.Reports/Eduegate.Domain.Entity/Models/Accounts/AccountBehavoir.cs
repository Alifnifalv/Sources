using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AccountBehavoir
    {
        public AccountBehavoir()
        {
            this.Accounts = new List<Account>();
        }

        public byte AccountBehavoirID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
