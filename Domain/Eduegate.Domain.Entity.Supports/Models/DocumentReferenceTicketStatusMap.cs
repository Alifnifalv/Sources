using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("DocumentReferenceTicketStatusMap", Schema = "cs")]
    public partial class DocumentReferenceTicketStatusMap
    {
        [Key]
        public long DocumentReferenceTicketStatusMapIID { get; set; }
        public int ReferenceTypeID { get; set; }
        public byte TicketStatusID { get; set; }

        public virtual DocumentReferenceType ReferenceType { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
    }
}