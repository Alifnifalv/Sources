using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketFeeDueMaps", Schema = "cs")]
    public partial class TicketFeeDueMap
    {
        [Key]
        public long TicketFeeDueMapIID { get; set; }

        public long? TicketID { get; set; }

        public long? StudentFeeDueID { get; set; }

        public decimal? FeeDueAmount { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual StudentFeeDue StudentFeeDue { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
