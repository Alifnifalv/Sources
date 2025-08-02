using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketCommunications", Schema = "cs")]
    public partial class TicketCommunication
    {
        [Key]
        public long TicketCommunicationIID { get; set; }
        public long? TicketID { get; set; }
        public long? LoginID { get; set; }
        public string Notes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CommunicationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FollowUpDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("LoginID")]
        [InverseProperty("TicketCommunications")]
        public virtual Login Login { get; set; }
        [ForeignKey("TicketID")]
        [InverseProperty("TicketCommunications")]
        public virtual Ticket Ticket { get; set; }
    }
}
