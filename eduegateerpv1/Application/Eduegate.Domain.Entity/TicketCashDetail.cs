namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.TicketCashDetails")]
    public partial class TicketCashDetail
    {
        [Key]
        public long CashDetailID { get; set; }

        public decimal RefundAmount { get; set; }

        public long ReferenceDetailID { get; set; }
    }
}
