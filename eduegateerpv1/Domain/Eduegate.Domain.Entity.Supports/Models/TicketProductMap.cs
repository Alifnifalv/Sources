using Eduegate.Domain.Entity.Supports.Models.Catalogs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
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
        //public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Quantity { get; set; }
        public long? TransactionDetailID { get; set; }
        public string SerialNumber { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}