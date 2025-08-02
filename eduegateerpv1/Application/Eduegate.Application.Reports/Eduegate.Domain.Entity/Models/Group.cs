using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Group
    {
        public Group()
        {
            this.Accounts = new List<Account>();
        }

        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
