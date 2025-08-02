using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerProductReferences", Schema = "catalog")]
    public partial class CustomerProductReference
    {
        [Key]
        public long CustomerProductReferenceIID { get; set; }
        public long? CustomerID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [StringLength(50)]
        public string BarCode { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("CustomerProductReferences")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("CustomerProductReferences")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
