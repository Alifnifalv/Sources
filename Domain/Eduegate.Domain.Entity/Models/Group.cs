using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Groups", Schema = "account")]
    public partial class Group
    {
        public Group()
        {
            this.Accounts = new List<Account>();
        }

        [Key]
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
