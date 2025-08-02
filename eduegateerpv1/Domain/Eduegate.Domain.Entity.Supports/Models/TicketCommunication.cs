using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketCommunications", Schema = "cs")]
    public partial class TicketCommunication
    {
        [Key]
        public long TicketCommunicationIID { get; set; }

        public long? TicketID { get; set; }

        public long? LoginID { get; set; }

        public string Notes { get; set; }

        public DateTime? CommunicationDate { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Login Login { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
