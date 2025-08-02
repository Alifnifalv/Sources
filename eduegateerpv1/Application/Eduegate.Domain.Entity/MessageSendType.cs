namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.MessageSendTypes")]
    public partial class MessageSendType
    {
        [Key]
        public int TypeID { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}
