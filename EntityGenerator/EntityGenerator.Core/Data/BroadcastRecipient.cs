using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BroadcastRecipients", Schema = "chat")]
    public partial class BroadcastRecipient
    {
        [Key]
        public long BroadcastRecipientIID { get; set; }
        public long BroadcastID { get; set; }
        public long ToLoginID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? StudentID { get; set; }

        [ForeignKey("BroadcastID")]
        [InverseProperty("BroadcastRecipients")]
        public virtual BroadCast Broadcast { get; set; }
    }
}
