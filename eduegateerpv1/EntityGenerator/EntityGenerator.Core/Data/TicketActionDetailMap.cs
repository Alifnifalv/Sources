using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketActionDetailMaps", Schema = "cs")]
    public partial class TicketActionDetailMap
    {
        public TicketActionDetailMap()
        {
            TicketActionDetailDetailMaps = new HashSet<TicketActionDetailDetailMap>();
        }

        [Key]
        public long TicketActionDetailIID { get; set; }
        public long TicketID { get; set; }
        public int? RefundTypeID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? RefundAmount { get; set; }
        [StringLength(200)]
        public string Reason { get; set; }
        [StringLength(200)]
        public string Remark { get; set; }
        [StringLength(50)]
        public string ReturnNumber { get; set; }
        public int? GiveItemTo { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? IssueType { get; set; }
        public long? AssignedEmployee { get; set; }
        public byte[] Timestamps { get; set; }

        [ForeignKey("TicketID")]
        [InverseProperty("TicketActionDetailMaps")]
        public virtual Ticket Ticket { get; set; }
        [InverseProperty("TicketActionDetailMap")]
        public virtual ICollection<TicketActionDetailDetailMap> TicketActionDetailDetailMaps { get; set; }
    }
}
