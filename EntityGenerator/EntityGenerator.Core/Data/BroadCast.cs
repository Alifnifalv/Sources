using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string BroadcastName { get; set; }

        [InverseProperty("Broadcast")]
        public virtual ICollection<BroadcastRecipient> BroadcastRecipients { get; set; }
    }
}
