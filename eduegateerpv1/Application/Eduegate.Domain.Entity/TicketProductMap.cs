namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.TicketProductMaps")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public decimal? Quantity { get; set; }

        public long? TransactionDetailID { get; set; }

        public string SerialNumber { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
