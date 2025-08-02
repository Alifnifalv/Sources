using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BroadCasts", Schema = "chat")]
    public partial class BroadCast
    {
        public BroadCast()
        {
            BroadcastRecipients = new HashSet<BroadcastRecipient>();
        }

        [Key]
        public long BroadcastIID { get; set; }

        public long FromLoginID { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string BroadcastName { get; set; }

        public virtual ICollection<BroadcastRecipient> BroadcastRecipients { get; set; }


    }
}