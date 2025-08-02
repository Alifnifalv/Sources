namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.TicketRefundDetails")]
    public partial class TicketRefundDetail
    {
        [Key]
        public long DetailIID { get; set; }

        public int RefundTypeID { get; set; }

        public long ReferenceTicketID { get; set; }

        public string ReturnNumber { get; set; }

        public int StatusID { get; set; }
    }
}
