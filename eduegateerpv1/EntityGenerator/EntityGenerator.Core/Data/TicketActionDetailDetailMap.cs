using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketActionDetailDetailMaps", Schema = "cs")]
    public partial class TicketActionDetailDetailMap
    {
        [Key]
        public long TicketActionDetailDetailMapIID { get; set; }
        public long TicketActionDetailMapID { get; set; }
        public int? Notify { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public byte[] Timestamps { get; set; }

        [ForeignKey("TicketActionDetailMapID")]
        [InverseProperty("TicketActionDetailDetailMaps")]
        public virtual TicketActionDetailMap TicketActionDetailMap { get; set; }
    }
}
