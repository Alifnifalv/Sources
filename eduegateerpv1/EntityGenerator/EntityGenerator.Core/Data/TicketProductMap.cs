using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketProductMaps", Schema = "cs")]
    public partial class TicketProductMap
    {
        [Key]
        public long TicketProductMapIID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        public short? ReasonID { get; set; }
        [StringLength(500)]
        public string Narration { get; set; }
        public long? TicketID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Quantity { get; set; }
        public long? TransactionDetailID { get; set; }
        [Unicode(false)]
        public string SerialNumber { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("TicketProductMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("TicketProductMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        [ForeignKey("TicketID")]
        [InverseProperty("TicketProductMaps")]
        public virtual Ticket Ticket { get; set; }
    }
}
