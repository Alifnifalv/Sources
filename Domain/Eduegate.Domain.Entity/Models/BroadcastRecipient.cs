using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BroadcastRecipients", Schema = "chat")]
    public partial class BroadcastRecipient
    {
        [Key]
        public long BroadcastRecipientIID { get; set; }

        public long BroadcastID { get; set; }

        public long ToLoginID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual BroadCast Broadcast { get; set; }

        public long? StudentID { get; set; }


        
    }
}