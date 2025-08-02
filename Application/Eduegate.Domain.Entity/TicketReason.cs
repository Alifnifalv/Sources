namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.TicketReasons")]
    public partial class TicketReason
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TicketReasonID { get; set; }

        [StringLength(50)]
        public string TicketReasonName { get; set; }
    }
}
