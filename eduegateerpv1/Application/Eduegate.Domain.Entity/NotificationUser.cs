namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.NotificationUsers")]
    public partial class NotificationUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NotificationUserID { get; set; }

        [Column("NotificationUser")]
        [StringLength(50)]
        public string NotificationUser1 { get; set; }
    }
}
