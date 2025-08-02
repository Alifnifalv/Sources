using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypes", Schema = "catalog")]
    public partial class DeliveryType
    {
        public DeliveryType()
        {
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        public short DeliveryTypeID { get; set; }
        [StringLength(50)]
        public string DeliveryMethod { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(3, 3)")]
        public decimal? DeliveryCost { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("DeliveryMethod")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
