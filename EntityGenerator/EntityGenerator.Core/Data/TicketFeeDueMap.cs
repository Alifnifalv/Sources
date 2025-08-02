using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketFeeDueMaps", Schema = "cs")]
    public partial class TicketFeeDueMap
    {
        [Key]
        public long TicketFeeDueMapIID { get; set; }
        public long? TicketID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? FeeDueAmount { get; set; }

        [ForeignKey("StudentFeeDueID")]
        [InverseProperty("TicketFeeDueMaps")]
        public virtual StudentFeeDue StudentFeeDue { get; set; }
        [ForeignKey("TicketID")]
        [InverseProperty("TicketFeeDueMaps")]
        public virtual Ticket Ticket { get; set; }
    }
}
