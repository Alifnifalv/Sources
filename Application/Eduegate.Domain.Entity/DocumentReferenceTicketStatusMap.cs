namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.DocumentReferenceTicketStatusMap")]
    public partial class DocumentReferenceTicketStatusMap
    {
        [Key]
        public long DocumentReferenceTicketStatusMapIID { get; set; }

        public int ReferenceTypeID { get; set; }

        public byte TicketStatusID { get; set; }

        public virtual DocumentReferenceType DocumentReferenceType { get; set; }

        public virtual TicketStatus TicketStatus { get; set; }
    }
}
