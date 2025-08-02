using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DocumentReferenceTicketStatusMap", Schema = "cs")]
    public partial class DocumentReferenceTicketStatusMap
    {
        [Key]
        public long DocumentReferenceTicketStatusMapIID { get; set; }
        public int ReferenceTypeID { get; set; }
        public byte TicketStatusID { get; set; }

        [ForeignKey("ReferenceTypeID")]
        [InverseProperty("DocumentReferenceTicketStatusMaps")]
        public virtual DocumentReferenceType ReferenceType { get; set; }
        [ForeignKey("TicketStatusID")]
        [InverseProperty("DocumentReferenceTicketStatusMaps")]
        public virtual TicketStatus TicketStatus { get; set; }
    }
}
